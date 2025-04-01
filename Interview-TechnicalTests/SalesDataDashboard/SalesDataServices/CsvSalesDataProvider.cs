using SalesDataInterfaces;
using SalesDataViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using NLog;

namespace SalesDataServices
{
    public class CsvSalesDataProvider : ISalesDataProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string _csvPath;

        public CsvSalesDataProvider(string csvPath)
        {
            _csvPath = csvPath;
        }

        public async Task<List<SaleDataRowModel>> GetSalesDataAsync()
        {
            var salesData = new List<SaleDataRowModel>();

            try
            {
                if (!File.Exists(_csvPath))
                {
                    Logger.Warn($"CSV file not found at path: {_csvPath}");
                    return salesData;
                }

                string[] lines;

                using (var reader = File.OpenText(_csvPath))
                {
                    var content = await reader.ReadToEndAsync();
                    lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                }

                for (var i = 1; i < lines.Length; i++)
                {
                    var cols = lines[i].Split(',');

                    if (cols.Length < 8)
                        continue;

                    salesData.Add(ParseRow(cols));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error reading CSV data from path: {_csvPath}");
            }

            return salesData;
        }

        private SaleDataRowModel ParseRow(string[] cols)
        {
            return new SaleDataRowModel
            {
                Segment = HtmlEncodeSafe(cols[0]),
                Country = HtmlEncodeSafe(cols[1]),
                Product = HtmlEncodeSafe(cols[2]),
                DiscountBand = HtmlEncodeSafe(cols[3]),
                UnitsSold = int.TryParse(cols[4], out int units) ? units : 0,
                ManufacturingPrice = decimal.TryParse(cols[5], out decimal mp) ? mp : 0,
                SalePrice = decimal.TryParse(cols[6], out decimal sp) ? sp : 0,
                Date = DateTime.TryParseExact(cols[7], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)
                        ? dt
                        : (DateTime?)null
            };
        }

        private string HtmlEncodeSafe(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : HttpUtility.HtmlEncode(input.Trim());
        }
    }
}
