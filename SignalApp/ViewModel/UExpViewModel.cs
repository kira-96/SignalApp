namespace SignalApp.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.Practices.ServiceLocation;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Collections.Generic;
    using SignalApp.NavigationService;

    public class UExpViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private double _A, _B, _t0, _tao;

        private int _Range;

        private AreaSeries Data;

        public double A
        {
            get { return _A; }
            set
            {
                if (value != _A)
                {
                    _A = value > 0 ? value : 1;
                    RaisePropertyChanged(() => A);
                    ReDraw();
                }
            }
        }

        public double B
        {
            get { return _B; }
            set
            {
                if (value != _B)
                {
                    _B = value >= 0 ? value : 1;
                    RaisePropertyChanged(() => B);
                    ReDraw();
                }
            }
        }

        public double t0
        {
            get { return _t0; }
            set
            {
                if (value != _t0)
                {
                    _t0 = value >= 0 ? value : 1;
                    RaisePropertyChanged(() => t0);
                    ReDraw();
                }
            }
        }

        public double tao
        {
            get { return _tao; }
            set
            {
                if (value != _tao)
                {
                    _tao = value > 0 ? value : 1;
                    RaisePropertyChanged(() => tao);
                    ReDraw();
                }
            }
        }

        public int Range
        {
            get { return _Range; }
            set
            {
                if (value != _Range)
                {
                    _Range = value >= 0 ? value : 2048;
                    RaisePropertyChanged(() => Range);
                    ReDraw();
                }
            }
        }

        public PlotModel plotModel { get; private set; }

        public RelayCommand HomeCommand { get; private set; }

        public UExpViewModel()
        {
            InitVar();
            InitPlotModel();
            HomeCommand = new RelayCommand(() => 
            {
                _Nav.NavigateTo(nameof(View.MainPage));
                plotModel = null;
                InitPlotModel();
            });
        }

        private void InitVar()
        {
            _A = 100;
            _B = 1;
            _t0 = 10;
            _tao = 100;
            _Range = 2048;
        }

        private void InitPlotModel()
        {
            plotModel = new PlotModel();
            Data = new AreaSeries()
            {
                Title = "负指数信号",
                Color = OxyColors.DodgerBlue,
                Color2 = OxyColors.Transparent
            };
            plotModel.Series.Add(Data);
            LinearAxis axisx = new LinearAxis()
            {
                Title = "i",
                Position = AxisPosition.Bottom
            };
            LinearAxis axisy = new LinearAxis()
            {
                Title = "Value",
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false
            };
            plotModel.Axes.Add(axisx);
            plotModel.Axes.Add(axisy);
            ReDraw();
        }

        private void ReDraw()
        {
            Random rand = new Random();
            List<DataPoint> Points1 = new List<DataPoint>();
            List<DataPoint> Points2 = new List<DataPoint>();

            for (int i = 0; i < Range; i++)
            {
                if (i < t0)
                {
                    Points1.Add(new DataPoint(i, 0));
                }
                else
                {
                    double r = rand.NextDouble() - 0.5;
                    double temp = A * Math.Exp((t0 - i) / tao) + B * r;
                    Points1.Add(new DataPoint(i, temp));
                }
                Points2.Add(new DataPoint(i, 0));
            }
            Data.Points.Clear();
            Data.Points2.Clear();
            Data.Points.AddRange(Points1);
            Data.Points2.AddRange(Points2);
            plotModel.InvalidatePlot(true);
        }
    }
}
