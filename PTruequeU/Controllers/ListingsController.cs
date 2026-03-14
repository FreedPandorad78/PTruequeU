using Microsoft.AspNetCore.Mvc;

namespace PTruequeU.Controllers
{
    public class ListingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
