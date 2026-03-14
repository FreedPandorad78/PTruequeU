using Microsoft.AspNetCore.Mvc;

namespace PTruequeU.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
