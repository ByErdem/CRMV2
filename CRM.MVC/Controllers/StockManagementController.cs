using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    public class StockManagementController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult WareHouse()
        {
            return View();
        }

        public IActionResult WarehouseTransfer()
        {
            return View();
        }
    }
}
