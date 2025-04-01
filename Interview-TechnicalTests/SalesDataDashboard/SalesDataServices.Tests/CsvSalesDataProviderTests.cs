using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesDataViewModels;

namespace SalesDataServices.Tests
{
    [TestClass]
    public class CsvSalesDataProviderTests
    {
        private string _testCsvPath;

        [TestInitialize]
        public void Setup()
        {
            var testDir = Path.Combine(Path.GetTempPath(), "SalesTest");
            Directory.CreateDirectory(testDir);

            _testCsvPath = Path.Combine(testDir, "Data.csv");

            File.WriteAllLines(_testCsvPath, new[]
            {
                "Segment,Country,Product,Discount Band,Units Sold,Manufacturing Price,Sale Price,Date",
                "Govt,<script>alert('x')</script>,Gadget,Low,10,5.00,7.00,01/01/2023"
            });
        }

        [TestMethod]
        public async Task GetSalesDataAsync_EncodesUnsafeCharacters()
        {
            var provider = new CsvSalesDataProvider(_testCsvPath);
            List<SaleDataRowModel> result = await provider.GetSalesDataAsync();

            Console.WriteLine("Actual value: " + result[0].Country);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("&lt;script&gt;alert(&#39;x&#39;)&lt;/script&gt;", result[0].Country);
        }


        [TestMethod]
        public async Task GetSalesDataAsync_ParsesValues_Correctly()
        {
            var provider = new CsvSalesDataProvider(_testCsvPath);
            List<SaleDataRowModel> result = await provider.GetSalesDataAsync();

            Assert.AreEqual("Govt", result[0].Segment);
            Assert.AreEqual("Gadget", result[0].Product);
            Assert.AreEqual("Low", result[0].DiscountBand);
            Assert.AreEqual(10, result[0].UnitsSold);
            Assert.AreEqual(5.00m, result[0].ManufacturingPrice);
            Assert.AreEqual(7.00m, result[0].SalePrice);
            Assert.AreEqual(new System.DateTime(2023, 1, 1), result[0].Date);
        }


        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testCsvPath))
                File.Delete(_testCsvPath);
        }
    }
}