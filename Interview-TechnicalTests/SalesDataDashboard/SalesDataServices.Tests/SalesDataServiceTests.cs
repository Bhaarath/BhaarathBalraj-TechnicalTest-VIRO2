using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalesDataInterfaces;
using SalesDataServices;
using SalesDataViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDataServices.Tests
{
    [TestClass]
    public class SalesDataServiceTests
    {
        [TestMethod]
        public async Task GetSalesDataAsync_ReturnsData_FromDataSource()
        {
            var mockSource = new Mock<ISalesDataProvider>();
            mockSource.Setup(s => s.GetSalesDataAsync()).ReturnsAsync(new List<SaleDataRowModel>
            {
                new SaleDataRowModel { Product = "Test Product", UnitsSold = 10 }
            });

            var service = new SalesDataService(mockSource.Object);
            var result = await service.GetSalesDataAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test Product", result[0].Product);
        }

       
    }
}