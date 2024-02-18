using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using MVCIntroduction.Models;

namespace MVCIntroduction.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }


        public IActionResult Numbers()
        {
            return View();
        }

        public IActionResult NumbersToN(int num)
        {
            ViewBag.Num = num;
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}