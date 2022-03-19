using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;

namespace HelloOxyPlot
{
    internal class PlotModelHandler
    {
        private readonly PlotModel _model;
        private readonly LineSeries _series;
        
        internal PlotModelHandler()
        {
            var XAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                TickStyle = TickStyle.Inside,
                Maximum = 1024,
                MajorStep = 256,
            };

            var YAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                TickStyle = TickStyle.Inside,
                Maximum = 4096,
                MajorStep = 1024,
            };

            _series = new LineSeries
            {
                Title = "Line",
                InterpolationAlgorithm = InterpolationAlgorithms.UniformCatmullRomSpline,//グラフの角を丸める
                Color = OxyColor.FromArgb(0xff, 0xff, 0, 0),     // 上の線の色
                StrokeThickness = 2,                             // 線の太さ
            };
            
            // 点を追加
            _series.Points.Add(new DataPoint(10, 123.4));
            
            var seriesConst = new LineSeries
            {
                Title = "threshold",
                InterpolationAlgorithm = InterpolationAlgorithms.UniformCatmullRomSpline,
                Color = OxyColor.FromRgb(128, 200, 0),
                StrokeThickness = 3,
            };
            seriesConst.Points.Add(new DataPoint(0, 3800));
            seriesConst.Points.Add(new DataPoint(1024, 3800));


            _model = new PlotModel();
            _model.Title = "グラフのタイトル";
            _model.Axes.Add(XAxis);
            _model.Axes.Add(YAxis);
            _model.Series.Add(_series);
            _model.Series.Add(seriesConst);
        }

        internal PlotModel Create() => _model;

        internal void Update()
        {
            var x = new Random().Next(0, 1023);
            var y = new Random().Next(0, 4065);
            _series.Points.Add(new DataPoint(x, y));

            _model.InvalidatePlot(true);
        }
    }
}
