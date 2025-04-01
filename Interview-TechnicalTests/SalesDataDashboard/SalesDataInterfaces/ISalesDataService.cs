using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesDataViewModels;

namespace SalesDataInterfaces
{
    public interface ISalesDataService
    {
        Task<List<SaleDataRowModel>> GetSalesDataAsync();

    }

    
}
