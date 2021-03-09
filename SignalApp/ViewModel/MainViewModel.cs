namespace SignalApp.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using SignalApp.NavigationService;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            LoadedCommand = new RelayCommand(Loaded);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private bool _CanGoBack;

        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        public bool CanGoBack
        {
            get { return _CanGoBack; }
            private set
            {
                _CanGoBack = value;
                RaisePropertyChanged(() => CanGoBack);
            }
        }

        private void Loaded()
        {
            _Nav.NavigateTo(nameof(View.MainPage));
        }
        private void GoBack()
        {
            _Nav.GoBack();
        }
    }
}