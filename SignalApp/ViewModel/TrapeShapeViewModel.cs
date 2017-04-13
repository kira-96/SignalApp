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

    public class TrapeShapeViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private int _A, _B, _T0, _Range, _Na, _Nb, _Nc;
        private double _Tao;

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
                    _T0 = value > _Nc + 2 ? value : _Nc + 2;
                    RaisePropertyChanged(() => T0);
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

        public int Na
        {
            get { return _Na; }
            set
            {
                if (value != _Na)
                {
                    _Na = value;
                    RaisePropertyChanged(() => Na);
                    ReDraw();
                }
            }
        }

        public int Nb
        {
            get { return _Nb; }
            set
            {
                if (value != _Nb)
                {
                    _Nb = value;
                    RaisePropertyChanged(() => Nb);
                    ReDraw();
                }
            }
        }

        public int Nc
        {
            get { return _Nc; }
            set
            {
                if (value != _Nc)
                {
                    _Nc = value;
                    RaisePropertyChanged(() => Nc);
                    ReDraw();
                }
            }
        }

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public TrapeShapeViewModel()
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
            _T0 = 500;
            _Tao = 100;
            _Range = 2048;
            _Na = 150;
            _Nb = 300;
            _Nc = 450;
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
                Color = OxyColors.Purple
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
            List<DataPoint> TrapePoints = new List<DataPoint>();

            Random Rand = new Random();

            for (int i = 0; i < Range; i++)
            {
                double r = Rand.NextDouble() - 0.5;
                double temp = (A * Math.Exp((T0 - i) / Tao) + B * r) * (i > T0 ? 1 : 0);
                InPoints.Add(new DataPoint(i, temp));
            }

            /*
            double TempValue1 = InPoints[0].Y;
            double TempValue2 = InPoints[0].Y;
            for (int i = 0; i < Range; i++)
            {
                // double temp = (InPoints[i].Y + 2 * K * TempValue1 - K * TempValue2) / (1 + K);
                double temp = ((K * (3 - a) + 2 * K * K) * TempValue1 - K * K * TempValue2 + a * InPoints[i].Y) / (1 + K * (3 - a) + K * K);
                TempValue2 = TempValue1;
                TempValue1 = temp;
                OutPoints.Add(new DataPoint(i, temp));
            }
            */

            double TempValue3 = 0;
            double TempValue4 = 0;
            double d = Math.Exp(-1 / Tao);
            for (int i = Nc + 2; i < Range; i++)
            {
                double temp = 2 * TempValue3 - TempValue4 + (InPoints[i - 1].Y - InPoints[i - Na - 1].Y - InPoints[i - Nb - 1].Y + InPoints[i - Nc - 1].Y - d * (InPoints[i - 2].Y - InPoints[i - Na - 2].Y - InPoints[i - Nb - 2].Y + InPoints[i - Nc - 2].Y)) / Na;
                TempValue4 = TempValue3;
                TempValue3 = temp;
                TrapePoints.Add(new DataPoint(i, temp));
            }

            InData.Points.Clear();
            InData.Points.AddRange(InPoints);
            OutData.Points.Clear();
            OutData.Points.AddRange(TrapePoints);

            _PlotModel.InvalidatePlot(true);
        }
    }
}
