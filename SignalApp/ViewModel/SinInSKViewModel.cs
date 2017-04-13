namespace SignalApp.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Practices.ServiceLocation;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Collections.Generic;
    using SignalApp.NavigationService;

    public class SinInSKViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private int _A, _B, _T, _C, _Range, _K;
        private double _a;

        public LineSeries InData { get; private set; }
        public LineSeries SKOutData { get; private set; }
        public LineSeries FIROutData { get; private set; }

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

        public int K
        {
            get { return _K; }
            set
            {
                if (value != _K)
                {
                    _K = value;
                    RaisePropertyChanged(() => K);
                    ReDraw();
                }
            }
        }

        public double a
        {
            get { return _a; }
            set
            {
                if (value != _a)
                {
                    _a = value;
                    RaisePropertyChanged(() => a);
                    ReDraw();
                }
            }
        }

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public SinInSKViewModel()
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
            _B = 5;
            _T = 360;
            _C = 100;
            _Range = 2048;
            _K = 25;
            _a = 1.15;
        }

        private void InitPlotModel()
        {
            _PlotModel = new PlotModel();
            InData = new LineSeries()
            {
                Title = "Vin",
                Color = OxyColors.DodgerBlue
            };
            SKOutData = new LineSeries()
            {
                Title = "SK-Vout",
                Color = OxyColors.Purple
            };
            FIROutData = new LineSeries()
            {
                Title = "FIR-Vout",
                Color = OxyColors.Orange
            };
            _PlotModel.Series.Add(InData);
            _PlotModel.Series.Add(SKOutData);
            _PlotModel.Series.Add(FIROutData);

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
            List<DataPoint> SKOutPoints = new List<DataPoint>();
            List<DataPoint> FIROutPoints = new List<DataPoint>();

            Random Rand = new Random();

            for (int i = 0; i < Range; i++)
            {
                double r = Rand.NextDouble() - 0.5;
                double temp = A * Math.Sin(2 * Math.PI * i / T) + C + B * r;
                InPoints.Add(new DataPoint(i, temp));
            }

            double TempValue1 = InPoints[0].Y;
            double TempValue2 = InPoints[0].Y;
            for (int i = 0; i < Range; i++)
            {
                // double temp = (InPoints[i].Y + 2 * K * TempValue1 - K * TempValue2) / (1 + K);
                double temp = ((K * (3 - a) + 2 * K * K) * TempValue1 - K * K * TempValue2 + a * InPoints[i].Y) / (1 + K * (3 - a) + K * K);
                TempValue2 = TempValue1;
                TempValue1 = temp;
                SKOutPoints.Add(new DataPoint(i, temp));
            }

            for (int i = 0; i < Range; i++)
            {
                if (i < 5)
                {
                    FIROutPoints.Add(new DataPoint(i, InPoints[i].Y));
                }
                else
                {
                    double temp = 0.15252 * (InPoints[i].Y + InPoints[i - 4].Y) + 0.24649 * (InPoints[i - 1].Y + InPoints[i - 3].Y) + 0.28299 * InPoints[i - 2].Y;
                    FIROutPoints.Add(new DataPoint(i, temp));
                }
            }

            InData.Points.Clear();
            InData.Points.AddRange(InPoints);
            SKOutData.Points.Clear();
            SKOutData.Points.AddRange(SKOutPoints);
            FIROutData.Points.Clear();
            FIROutData.Points.AddRange(FIROutPoints);

            _PlotModel.InvalidatePlot(true);
        }
    }
}
