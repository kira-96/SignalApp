namespace SignalApp.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();
            Closing += (s, e) => { ViewModel.ViewModelLocator.Cleanup(); };
        }
    }
}
