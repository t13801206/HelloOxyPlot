using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using OxyPlot.Legends;

namespace OxyCsvPlot.Models
{
    public class PlotModelHandler
    {
        private readonly CsvHandler _csvHandler1 = new CsvHandler("data1.csv");

        private readonly PlotModel _model = new PlotModel();
        private readonly DateTimeAxis _axisX = new DateTimeAxis();
        private readonly LinearAxis _axisY = new LinearAxis();

        private readonly LineSeries _series = new LineSeries();
        private readonly LineSeries _seriesThreshold = new LineSeries();

        private const int THRESHOLD = 3800;

        public PlotModelHandler()
        {
            SetModel();
        }

        public PlotModel Create() => _model;

        public enum Mode { I, V }
        private Mode _mode = Mode.I;

        public void ChangeMode()
        {
            _series.Points.Clear();
            _seriesThreshold.Points.Clear();

            if (_mode == Mode.I)
            {
                _axisX.Minimum = double.NaN;
                _axisX.Maximum = double.NaN;
                _axisX.StringFormat = "MM/dd-HH:mm";

                _mode = Mode.V;
            }
            else
            {
                _axisX.StringFormat = "yy/MM/dd-HH:mm";

                _mode = Mode.I;
            }

            _model.InvalidatePlot(true);
        }

        public void AddDataRange(DateTime startDate, DateTime endDate)
        {
            _axisX.Minimum = DateTimeAxis.ToDouble(startDate);
            _axisX.Maximum = DateTimeAxis.ToDouble(endDate);

            using var reader = _csvHandler1.OpenCsvReader();
            reader.Context.RegisterClassMap<CsvDataMap>();
            IEnumerable<CsvDataModel> records = reader.GetRecords<CsvDataModel>();

            var targetRecords = records
                .Where(x => x.DateTime.CompareTo(startDate) > 0)
                .Where(x => x.DateTime.CompareTo(endDate) < 0)
                .OrderBy(x => x.DateTime);

            foreach (var record in targetRecords)
            {
                _series.Points.Add(
                    new DataPoint(DateTimeAxis.ToDouble(record.DateTime), record.Data1));
            }

            _seriesThreshold.Points.Add(new DataPoint(_axisX.Minimum, THRESHOLD));
            _seriesThreshold.Points.Add(new DataPoint(_axisX.Maximum, THRESHOLD));

            _model.InvalidatePlot(true);
        }

        public void AddDataSingle()
        {
            using var reader = _csvHandler1.OpenCsvReader();
            reader.Context.RegisterClassMap<CsvDataMap>();
            IEnumerable<CsvDataModel> records = reader.GetRecords<CsvDataModel>();

            var targetRecord = records
                .OrderBy(x => x.DateTime)
                .Skip(_series.Points.Count)
                .First();

            _series.Points.Add(
                new DataPoint(DateTimeAxis.ToDouble(targetRecord.DateTime), targetRecord.Data1));

            _seriesThreshold.Points.Add(new DataPoint(_series.Points.First().X, THRESHOLD));
            _seriesThreshold.Points.Add(new DataPoint(_series.Points.Last().X, THRESHOLD));
            
            _model.InvalidatePlot(true);
        }

        private void SetModel()
        {
            _model.Legends.Add(
                new Legend()
                {
                    LegendPosition = LegendPosition.RightTop,
                    LegendBackground = OxyColor.FromRgb(200, 200, 200),
                    IsLegendVisible = true,
                });

            SetAxes();
            SetSeries();
        }

        private void SetSeries()
        {
            #region Series1
            _series.Title = "Series-1";
            _seriesThreshold.Color = OxyColor.FromRgb(0, 255, 0);
            _series.CanTrackerInterpolatePoints = false;
            _series.MarkerType = MarkerType.Circle;
            _series.MarkerSize = 3;
            #endregion

            #region Threshold
            _seriesThreshold.Title = "Threshold";
            _seriesThreshold.Color = OxyColor.FromRgb(255, 0, 0);
            _seriesThreshold.CanTrackerInterpolatePoints = false;
            #endregion

            _model.Series.Add(_series);
            _model.Series.Add(_seriesThreshold);
        }

        private void SetAxes()
        {
            #region X AXIS
            _axisX.Position = AxisPosition.Bottom;
            _axisX.TickStyle = TickStyle.Inside;
            //_axisX.Angle = 90;
            #endregion

            #region Y AXIS
            _axisY.Position = AxisPosition.Left;
            _axisY.TickStyle = TickStyle.Inside;
            _axisY.Minimum = 0;
            _axisY.Maximum = 4096;
            #endregion

            #region SET AXES
            _model.Axes.Add(_axisX);
            _model.Axes.Add(_axisY);
            #endregion
        }
    }
}
