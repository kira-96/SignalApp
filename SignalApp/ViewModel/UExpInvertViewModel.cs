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

    public class UExpInvertViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private int _A, _B, _T0, _Range;
        private double _Tao, _K1, _K2;

        private LineSeries InData;
        private AreaSeries OutData;

        public int A
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

        public int B
        {
            get { return _B; }
            set
            {
                if (value != _B)
                {
                    _B = value;
                    RaisePropertyChanged(() => B);
                    ReDraw();
                }
            }
        }

        public int T0
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

        public double Tao
        {
            get { return _Tao; }
            set
            {
                if (value != _Tao)
                {
                    _Tao = value;
                    RaisePropertyChanged(() => Tao);
                    ReDraw();
                }
            }
        }

        public double K1
        {
            get { return _K1; }
            set
            {
                if (value != _K1)
                {
                    _K1 = value;
                    RaisePropertyChanged(() => K1);
                    ReDraw();
                }
            }
        }

        public double K2
        {
            get { return _K2; }
            set
            {
                if (value != _K2)
                {
                    _K2 = value;
                    RaisePropertyChanged(() => K2);
                    ReDraw();
                }
            }
        }

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public UExpInvertViewModel()
        {
            InitVar();
            InitPlotModel();
            HomeCommand = new RelayCommand(() =>
            {
                _Nav.NavigateTo(nameof(View.MainPage));
                _PlotModel = null;
                InitPlotModel();
            });
        }

        private void InitVar()
        {
            _A = 100;
            _B = 3;
            _T0 = 10;
            _Range = 2048;
            _Tao = 100;
            _K1 = 0.01;
            _K2 = 0.01;
        }

        private void InitPlotModel()
        {
            _PlotModel = new PlotModel();
            InData = new LineSeries()
            {
                Title = "Vin",
                Color = OxyColors.DodgerBlue
            };
            OutData = new AreaSeries()
            {
                Title = "Vout",
                Color = OxyColors.Orange,
                Color2 = OxyColors.Transparent
            };
            _PlotModel.Series.Add(InData);
            _PlotModel.Series.Add(OutData);

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
            ReDraw();
        }

        private void ReDraw()
        {
            List<DataPoint> InPoints = new List<DataPoint>();
            List<DataPoint> OutPoints = new List<DataPoint>();
            List<DataPoint> Points2 = new List<DataPoint>();

            Random Rand = new Random();

            for (int i = 0; i < Range; i++)
            {
                double r = Rand.NextDouble() - 0.5;
                double temp = (A * (Math.Exp((T0 - i) / Tao)) + B * r) * (i > T0 ? 1 : 0);
                InPoints.Add(new DataPoint(i, temp));
            }

            double TempValue = 0;
            for (int i = 1; i < Range; i++)
            {
                double temp = (TempValue - K1 * InPoints[i].Y) / (1 + K2);
                TempValue = temp;
                OutPoints.Add(new DataPoint(i, temp));
                Points2.Add(new DataPoint(i, 0));
            }

            InData.Points.Clear();
            InData.Points.AddRange(InPoints);
            OutData.Points.Clear();
            OutData.Points2.Clear();
            OutData.Points.AddRange(OutPoints);
            OutData.Points2.AddRange(Points2);

            _PlotModel.InvalidatePlot(true);
        }
    }
}
