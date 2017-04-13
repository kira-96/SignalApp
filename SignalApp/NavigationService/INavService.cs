namespace SignalApp.NavigationService
{
    using GalaSoft.MvvmLight.Views;

    public interface INavService : INavigationService
    {
        object Parameter { get; }
        bool CanBack { get; }
        bool CanGoBack();
    }
}
