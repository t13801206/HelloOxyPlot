using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace OxyCsvPlot.Models
{
    public class CsvHandler
    {
        private readonly string _csvPath;

        public CsvHandler(string csvPath)
        {
            _csvPath = csvPath;

            FileCheck();
        }

        public CsvReader OpenCsvReader()
        {
            var stream = new StreamReader(_csvPath, Encoding.UTF8);
            var cultureInfo = new CultureInfo("ja-JP");
            var reader = new CsvReader(stream, cultureInfo);

            return reader;
        }
        
        private void FileCheck()
        {
            if (!File.Exists(_csvPath))
                throw new FileNotFoundException($"File Not Found!! --->>> {_csvPath}");

            if (!_csvPath.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
                throw new FormatException($"Invalid Extension!! --->>> {_csvPath}");
        }
    }
}
