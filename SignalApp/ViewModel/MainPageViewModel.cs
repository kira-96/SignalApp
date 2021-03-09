namespace SignalApp.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using SignalApp.NavigationService;

    public class MainPageViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        public RelayCommand<object> TileCommand { get; private set; }

        public MainPageViewModel()
        {
            TileCommand = new RelayCommand<object>(TileClick);
        }

        private void TileClick(object tag)
        {
            if (tag.ToString().Equals("UExp"))
            {
                _Nav.NavigateTo(nameof(View.UExpPage));
            }
            else if (tag.ToString().Equals("DoubleExp"))
            {
                _Nav.NavigateTo(nameof(View.DoubleExpPage));
            }
            else if (tag.ToString().Equals("SquareIn"))
            {
                _Nav.NavigateTo(nameof(View.SquareInPage));
            }
            else if (tag.ToString().Equals("StepIn"))
            {
                _Nav.NavigateTo(nameof(View.StepInPage));
            }
            else if (tag.ToString().Equals("UExpIn"))
            {
                _Nav.NavigateTo(nameof(View.UExpInPage));
            }
            else if (tag.ToString().Equals("DoubleExpIn"))
            {
                _Nav.NavigateTo(nameof(View.DoubleExpInPage));
            }
            else if (tag.ToString().Equals("StepInRC"))
            {
                _Nav.NavigateTo(nameof(View.StepInRCPage));
            }
            else if (tag.ToString().Equals("UExpInRC"))
            {
                _Nav.NavigateTo(nameof(View.UExpInRCPage));
            }
            else if (tag.ToString().Equals("UExpInvert"))
            {
                _Nav.NavigateTo(nameof(View.UExpInvertPage));
            }
            else if (tag.ToString().Equals("UExpInPZC"))
            {
                _Nav.NavigateTo(nameof(View.UExpInPZCPage));
            }
            else if (tag.ToString().Equals("SinInLC"))
            {
                _Nav.NavigateTo(nameof(View.SinInLCPage));
            }
            else if (tag.ToString().Equals("SinInSK"))
            {
                _Nav.NavigateTo(nameof(View.SinInSKPage));
            }
            else if (tag.ToString().Equals("UExpInSK"))
            {
                _Nav.NavigateTo(nameof(View.UExpInSKPage));
            }
            else if (tag.ToString().Equals("Trapezoidal"))
            {
                _Nav.NavigateTo(nameof(View.TrapeShapePage));
            }
            else {}
        }
    }
}
