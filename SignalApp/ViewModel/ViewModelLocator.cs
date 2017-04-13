/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SignalApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

namespace SignalApp.ViewModel
{
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using SignalApp.NavigationService;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<UExpViewModel>();
            SimpleIoc.Default.Register<DoubleExpViewModel>();
            SimpleIoc.Default.Register<SquareInViewModel>();
            SimpleIoc.Default.Register<StepInViewModel>();
            SimpleIoc.Default.Register<UExpInViewModel>();
            SimpleIoc.Default.Register<DoubleExpInViewModel>();
            SimpleIoc.Default.Register<StepInRCViewModel>();
            SimpleIoc.Default.Register<UExpInRCViewModel>();
            SimpleIoc.Default.Register<UExpInvertViewModel>();
            SimpleIoc.Default.Register<UExpInPZCViewModel>();
            SimpleIoc.Default.Register<SinInLCViewModel>();
            SimpleIoc.Default.Register<SinInSKViewModel>();
            SimpleIoc.Default.Register<UExpInSKViewModel>();
            SimpleIoc.Default.Register<TrapeShapeViewModel>();
            SimpleIoc.Default.Register<INavService>(() => InitNavigationService());
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MainPageViewModel MainPage => ServiceLocator.Current.GetInstance<MainPageViewModel>();
        public UExpViewModel UExpPage => ServiceLocator.Current.GetInstance<UExpViewModel>();
        public DoubleExpViewModel DoubleExpPage => ServiceLocator.Current.GetInstance<DoubleExpViewModel>();
        public SquareInViewModel SquareIn => ServiceLocator.Current.GetInstance<SquareInViewModel>();
        public StepInViewModel StepIn => ServiceLocator.Current.GetInstance<StepInViewModel>();
        public UExpInViewModel UExpIn => ServiceLocator.Current.GetInstance<UExpInViewModel>();
        public DoubleExpInViewModel DoubleExpIn => ServiceLocator.Current.GetInstance<DoubleExpInViewModel>();
        public StepInRCViewModel StepInRC => ServiceLocator.Current.GetInstance<StepInRCViewModel>();
        public UExpInRCViewModel UExpInRC => ServiceLocator.Current.GetInstance<UExpInRCViewModel>();
        public UExpInvertViewModel UExpInvert => ServiceLocator.Current.GetInstance<UExpInvertViewModel>();
        public UExpInPZCViewModel UExpInPZC => ServiceLocator.Current.GetInstance<UExpInPZCViewModel>();
        public SinInLCViewModel SinInLC => ServiceLocator.Current.GetInstance<SinInLCViewModel>();
        public SinInSKViewModel SinInSK => ServiceLocator.Current.GetInstance<SinInSKViewModel>();
        public UExpInSKViewModel UExpInSK => ServiceLocator.Current.GetInstance<UExpInSKViewModel>();
        public TrapeShapeViewModel TrapeShape => ServiceLocator.Current.GetInstance<TrapeShapeViewModel>();

        private INavService InitNavigationService()
        {
            NavigationService nav = new NavigationService();
            nav.Configure(nameof(View.MainPage), new Uri("pack://application:,,,/View/MainPage.xaml"));
            nav.Configure(nameof(View.UExpPage), new Uri("pack://application:,,,/View/UExpPage.xaml"));
            nav.Configure(nameof(View.DoubleExpPage), new Uri("pack://application:,,,/View/DoubleExpPage.xaml"));
            nav.Configure(nameof(View.SquareInPage), new Uri("pack://application:,,,/View/SquareInPage.xaml"));
            nav.Configure(nameof(View.StepInPage), new Uri("pack://application:,,,/View/StepInPage.xaml"));
            nav.Configure(nameof(View.UExpInPage), new Uri("pack://application:,,,/View/UExpInPage.xaml"));
            nav.Configure(nameof(View.DoubleExpInPage), new Uri("pack://application:,,,/View/DoubleExpInPage.xaml"));
            nav.Configure(nameof(View.StepInRCPage), new Uri("pack://application:,,,/View/StepInRCPage.xaml"));
            nav.Configure(nameof(View.UExpInRCPage), new Uri("pack://application:,,,/View/UExpInRCPage.xaml"));
            nav.Configure(nameof(View.UExpInvertPage), new Uri("pack://application:,,,/View/UExpInvertPage.xaml"));
            nav.Configure(nameof(View.UExpInPZCPage), new Uri("pack://application:,,,/View/UExpInPZCPage.xaml"));
            nav.Configure(nameof(View.SinInLCPage), new Uri("pack://application:,,,/View/SinInLCPage.xaml"));
            nav.Configure(nameof(View.SinInSKPage), new Uri("pack://application:,,,/View/SinInSKPage.xaml"));
            nav.Configure(nameof(View.UExpInSKPage), new Uri("pack://application:,,,/View/UExpInSKPage.xaml"));
            nav.Configure(nameof(View.TrapeShapePage), new Uri("pack://application:,,,/View/TrapeShapePage.xaml"));
            return nav;
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
            ServiceLocator.Current.GetInstance<MainViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<MainPageViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<DoubleExpViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<SquareInViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<StepInViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpInViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<DoubleExpInViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<StepInRCViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpInRCViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpInvertViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpInPZCViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<SinInLCViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<SinInSKViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<UExpInSKViewModel>().Cleanup();
            ServiceLocator.Current.GetInstance<TrapeShapeViewModel>().Cleanup();
        }
    }
}