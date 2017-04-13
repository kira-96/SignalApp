namespace SignalApp.NavigationService
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Media;

    public class NavigationService : ViewModelBase, INavService
    {
        private readonly Dictionary<string, Uri> _PagesByKey;
        private readonly List<string> _Historic;
        private string _CurrentPageKey;
        private bool _CanBack;
        public object Parameter { get; private set; }

        public NavigationService()
        {
            _PagesByKey = new Dictionary<string, Uri>();
            _Historic = new List<string>();
        }

        public string CurrentPageKey
        {
            get { return _CurrentPageKey; }
            private set
            {
                _CurrentPageKey = value;
                RaisePropertyChanged(() => CurrentPageKey);
            }
        }

        public bool CanBack
        {
            get { return _CanBack; }
            private set
            {
                _CanBack = value;
                RaisePropertyChanged(() => CanBack);
            }
        }

        public bool CanGoBack()
        {
            return _Historic.Count > 1;
        }

        public void GoBack()
        {
            if (_Historic.Count > 1)
            {
                _Historic.Remove(_Historic.Last());
                NavigateTo(_Historic.Last(), "Back");
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, "Next");
        }

        public virtual void NavigateTo(string pageKey, object parameter)
        {
            lock (_PagesByKey)
            {
                if (!_PagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"No such page: {pageKey}", nameof(pageKey));
                }
                Frame frame = GetDescendantFromName(Application.Current.MainWindow, "MainFrame") as Frame;
                if (frame != null)
                {
                    frame.Source = _PagesByKey[pageKey];
                    // frame.Navigate(_PagesByKey[pageKey]);
                }
                Parameter = parameter;
                if (parameter.ToString().Equals("Next"))
                {
                    _Historic.Add(pageKey);
                }
                CurrentPageKey = pageKey;
                CanBack = CanGoBack();
            }
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_PagesByKey)
            {
                if (_PagesByKey.ContainsKey(key))
                {
                    _PagesByKey[key] = pageType;
                }
                else
                {
                    _PagesByKey.Add(key, pageType);
                }
            }
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                FrameworkElement frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }
    }
}
