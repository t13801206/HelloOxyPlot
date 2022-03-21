using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxyCsvPlot.Models
{
    internal class CsvDataModel
    {
        public DateTime DateTime { get; set; }
        public double Data1 { get; set; }
        public double Data2 { get; set; }
        public double Data3 { get; set; }
    }

    internal sealed class CsvDataMap : ClassMap<CsvDataModel>
    {
        public CsvDataMap()
        {
            Map(x => x.DateTime).Index(0);
            Map(x => x.Data1).Index(1);
            Map(x => x.Data2).Index(2);
            Map(x => x.Data3).Index(3);
        }
    }
}
