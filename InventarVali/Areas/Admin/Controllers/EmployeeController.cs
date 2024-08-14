using Microsoft.AspNetCore.Mvc;

namespace InventarVali.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
