using System;
using OxyPlot;
using OxyPlot.Series;
using Reactive.Bindings;

namespace HelloOxyPlot
{
    public class MainWindowViewModel
    {
        public PlotModel MyModel1 { get; }
        public PlotModel MyModel2 { get; }
        public PlotModel MyModel3 { get; }

        public ReactiveCommand UpdateGraphCommand1 { get; }
        public ReactiveCommand UpdateGraphCommand2 { get; }
        public ReactiveCommand UpdateGraphCommand3 { get; }

        private readonly LineSeries _lineSeries = new LineSeries();
        private readonly PlotModelHandler _plotModelHandler = new PlotModelHandler();

        public MainWindowViewModel()
        {
            MyModel1 = new PlotModel { Title = "Example 1" };
            MyModel1.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            MyModel2 = new PlotModel();
            MyModel2.Series.Add(_lineSeries);

            MyModel3 = _plotModelHandler.Create();

            UpdateGraphCommand1 = new ReactiveCommand().WithSubscribe(UpdateGraph1);
            UpdateGraphCommand2 = new ReactiveCommand().WithSubscribe(UpdateGraph2);
            UpdateGraphCommand3 = new ReactiveCommand().WithSubscribe(UpdateGraph3);
        }

        private void UpdateGraph1()
        {
            MyModel1.Series.Clear();
            MyModel1.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));
            MyModel1.InvalidatePlot(true);
        }

        private void UpdateGraph2()
        {
            var x = DateTime.Now.Second;
            var y = new Random().Next(-100, 100);

            _lineSeries.Points.Add(new DataPoint(x, y));

            MyModel2.InvalidatePlot(true);
        }

        private void UpdateGraph3() => _plotModelHandler.Update();
    }
}
