namespace SignalApp.ViewModel
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System.Collections.Generic;
    using SignalApp.NavigationService;

    public class SquareInViewModel : ViewModelBase
    {
        private INavService _Nav => ServiceLocator.Current.GetInstance<INavService>();

        private int _A, _T, _Range;
        private double _K;
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

        public double K
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

        public PlotModel _PlotModel { get; private set; }
        public RelayCommand HomeCommand { get; private set; }

        public SquareInViewModel()
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
            _T = 1024;
            _Range = 4096;
            _K = 0.01;
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
            // _PlotModel.LegendPosition = LegendPosition.RightTop;
            ReDraw();
        }

        private void ReDraw()
        {
            List<DataPoint> InPoints = new List<DataPoint>();
            List<DataPoint> OutPoints = new List<DataPoint>();
            List<DataPoint> Points2 = new List<DataPoint>();

            for (int Counter = 0, i = 0; i < Range; i++, Counter = i % T)
            {
                int temp = A * (Counter < T / 2 ? 0 : 1);
                InPoints.Add(new DataPoint(i, temp));
            }

            double TempValue = 0;
            for (int i = 1; i < Range; i++)
            {
                double temp = (TempValue + InPoints[i].Y - InPoints[i - 1].Y) / (1 + K);
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
