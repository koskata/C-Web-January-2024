using System.Diagnostics;

using HouseRentingSystem.Models;
using HouseRentingSystem.Core.Models.Home;

using Microsoft.AspNetCore.Mvc;
using HouseRentingSystem.Core.Services.House;
using HouseRentingSystem.Core.Contacts.House;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService _houseService)
        {
            houseService = _houseService;
        }

        public async Task<IActionResult> Index()
        {
            var lastThreeHouses = await houseService.LastThreeHouses();

            return View(lastThreeHouses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}