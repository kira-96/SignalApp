namespace SignalApp.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Collections.Generic;
    using SignalApp.NavigationService;

    public class DoubleExpViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private double _A, _T0, _Ta, _Tb;
        private int _Range;
        private AreaSeries Data;

        public double A
        {
            get { return _A; }
            set
            {
                if (value != _A)
                {
                    _A = value;
                    RaisePropertyChanged(() => A);
                    ReDraw();
                }
            }
        }

        public double T0
        {
            get { return _T0; }
            set
            {
                if (value != _T0)
                {
                    _T0 = value;
                    RaisePropertyChanged(() => T0);
                    ReDraw();
                }
            }
        }

        public double Ta
        {
            get { return _Ta; }
            set
            {
                if (value != _Ta)
                {
                    _Ta = value;
                    RaisePropertyChanged(() => Ta);
                    ReDraw();
                }
            }
        }

        public double Tb
        {
            get { return _Tb; }
            set
            {
                if (value != _Tb)
                {
                    _Tb = value;
                    RaisePropertyChanged(() => Tb);
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
                    _Range = value;
                    RaisePropertyChanged(() => Range);
                    ReDraw();
                }
            }
        }

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public DoubleExpViewModel()
        {
            InitVar();
            InitPlotModel();
            HomeCommand = new RelayCommand(() => 
            {
                _Nav.GoBack();
                _PlotModel = null;
                InitPlotModel();
            });
        }

        private void InitVar()
        {
            _A = 100;
            _T0 = 10;
            _Ta = 100;
            _Tb = 10;
            _Range = 2048;
        }

        private void InitPlotModel()
        {
            _PlotModel = new PlotModel(); 
            Data = new AreaSeries()
            {
                Title = "双指数信号",
                Color = OxyColors.DodgerBlue,
                Color2 = OxyColors.Transparent
            };
            _PlotModel.Series.Add(Data);

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
            _PlotModel.Axes.Add(axisx);
            _PlotModel.Axes.Add(axisy);
            _PlotModel.LegendPosition = LegendPosition.RightTop;
            ReDraw();
        }

        private void ReDraw()
        {
            // Random Rand = new Random();
            List<DataPoint> Points1 = new List<DataPoint>();
            List<DataPoint> Points2 = new List<DataPoint>();

            for (int i = 0; i < Range; i++)
            {
                double temp = A * (Math.Exp((T0 - i) / Ta) - Math.Exp((T0 - i) / Tb)) * (i > T0 ? 1 : 0);
                Points1.Add(new DataPoint(i, temp));
                Points2.Add(new DataPoint(i, 0));
            }

            Data.Points.Clear();
            Data.Points2.Clear();
            Data.Points.AddRange(Points1);
            Data.Points2.AddRange(Points2);
            _PlotModel.InvalidatePlot(true);
        }
    }
}
