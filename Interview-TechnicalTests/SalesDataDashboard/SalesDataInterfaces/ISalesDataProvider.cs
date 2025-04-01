using SalesDataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDataInterfaces
{
    public interface ISalesDataProvider
    {
        Task<List<SaleDataRowModel>> GetSalesDataAsync();
    }

}
