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

    public class SinInLCViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private int _A, _T, _C, _Range, _Klc;

        public LineSeries InData { get; private set; }
        public LineSeries OutData { get; private set; }

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

        public int T
        {
            get { return _T; }
            set
            {
                if (value != _T)
                {
                    _T = value;
                    RaisePropertyChanged(() => T);
                    ReDraw();
                }
            }
        }

        public int C
        {
            get { return _C; }
            set
            {
                if (value != _C)
                {
                    _C = value;
                    RaisePropertyChanged(() => C);
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

        public int Klc
        {
            get { return _Klc; }
            set
            {
                if (value != _Klc)
                {
                    _Klc = value;
                    RaisePropertyChanged(() => Klc);
                    ReDraw();
                }
            }
        }

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public SinInLCViewModel()
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
            _T = 360;
            _C = 100;
            _Range = 2048;
            _Klc = 100000;
        }

        private void InitPlotModel()
        {
            _PlotModel = new PlotModel();
            InData = new LineSeries()
            {
                Title = "Vin",
                Color = OxyColors.DodgerBlue
            };
            OutData = new LineSeries()
            {
                Title = "Vout",
                Color = OxyColors.Orange
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

            Random Rand = new Random();

            for (int i = 0; i < Range; i++)
            {
                double r = Rand.NextDouble() - 0.5;
                // double temp = (A * (Math.Exp((T0 - i) / Tao)) + B * r) * (i > T0 ? 1 : 0);
                double temp = A * Math.Sin(2 * Math.PI * i / T) + C;
                InPoints.Add(new DataPoint(i, temp));
            }

            double TempValue1 = InPoints[0].Y;
            double TempValue2 = InPoints[0].Y;
            for (int i = 0; i < Range; i++)
            {
                // double temp = ((1 + K1) * InPoints[i].Y + TempValue - InPoints[i - 1].Y) / (1 + K1 + K2);
                double temp = (InPoints[i].Y + 2 * Klc * TempValue1 - Klc * TempValue2) / (1 + Klc);
                TempValue2 = TempValue1;
                TempValue1 = temp;
                OutPoints.Add(new DataPoint(i, temp));
            }

            InData.Points.Clear();
            InData.Points.AddRange(InPoints);
            OutData.Points.Clear();
            OutData.Points.AddRange(OutPoints);

            _PlotModel.InvalidatePlot(true);
        }
    }
}
