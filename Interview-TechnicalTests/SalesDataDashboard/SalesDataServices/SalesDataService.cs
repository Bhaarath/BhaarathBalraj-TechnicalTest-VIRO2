using SalesDataInterfaces;
using SalesDataViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesDataServices
{
    public class SalesDataService : ISalesDataService
    {
        private readonly ISalesDataProvider _salesDataProvider;

        public SalesDataService(ISalesDataProvider salesDataProvider )
        {
            _salesDataProvider = salesDataProvider;
        }

        public Task<List<SaleDataRowModel>> GetSalesDataAsync()
        {
            return _salesDataProvider.GetSalesDataAsync();
        }
    }
}