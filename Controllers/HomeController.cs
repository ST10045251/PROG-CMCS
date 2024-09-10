using Microsoft.AspNetCore.Mvc;

namespace CMCSPrototype.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Lecturer()
        {
            return View();
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult ClaimStatus()
        {
            return View();
        }

    }
}
