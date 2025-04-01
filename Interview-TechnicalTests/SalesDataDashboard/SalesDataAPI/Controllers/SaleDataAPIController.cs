using SalesDataInterfaces;
using System.Collections.Generic;
using System.Web.Http;
using SalesDataViewModels;
using System.Threading.Tasks;

namespace SalesDataAPI.Controllers
{
    [RoutePrefix("api/sales")]
    public class SaleDataAPIController : ApiController
    {
        private readonly ISalesDataService _salesService;

        public SaleDataAPIController(ISalesDataService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllSales()
        {
            var data = await _salesService.GetSalesDataAsync();
            return Ok(data);
        }
    }
}