using Microsoft.AspNetCore.Mvc;

namespace PTruequeU.Controllers
{
    public class TruequeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
