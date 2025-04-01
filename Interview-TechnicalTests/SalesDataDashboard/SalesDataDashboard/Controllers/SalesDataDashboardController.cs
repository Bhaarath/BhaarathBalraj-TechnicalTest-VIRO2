using System.Web.Mvc;

namespace SalesDataDashboard.Controllers
{
    public class SalesDataDashboardController : Controller
    {
        // Default landing page (optional)
        public ActionResult Index()
        {
            return RedirectToAction("DataTableView");
        }

        public ActionResult KendoGridView()
        {
            return View();
        }

        public ActionResult DataTableView()
        {
            return View();
        }
    }
}