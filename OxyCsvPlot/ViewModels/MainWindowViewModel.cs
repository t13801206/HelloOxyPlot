using OxyPlot;
using OxyPlot.Series;
using Prism.Mvvm;
using System;
using OxyCsvPlot.Models;
using Reactive.Bindings;

namespace OxyCsvPlot.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public PlotModel MyModel { get; }
        public ReactiveCommand ChangeModeCommand { get; }
        public ReactiveCommand AddDataRange { get; }
        public ReactiveCommand AddDataSingle { get; }

        public ReactiveProperty<DateTime> StartDate { get; set; } = new ReactiveProperty<DateTime>(DateTime.Now);
        public ReactiveProperty<DateTime> EndDate { get; set; } = new ReactiveProperty<DateTime>(DateTime.Now.AddYears(1).AddMonths(6));

        private readonly PlotModelHandler _plotModelHandler = new PlotModelHandler();

        public MainWindowViewModel()
        {
            MyModel = _plotModelHandler.Create();

            ChangeModeCommand = new ReactiveCommand().WithSubscribe(() =>
            {
                _plotModelHandler.ChangeMode();
            });
            
            AddDataSingle = new ReactiveCommand().WithSubscribe(() =>
            {
                _plotModelHandler.AddDataSingle();
            });
            
            AddDataRange = new ReactiveCommand().WithSubscribe(() =>
            {
                _plotModelHandler.AddDataRange(StartDate.Value, EndDate.Value);
            });
        }
    }
}
