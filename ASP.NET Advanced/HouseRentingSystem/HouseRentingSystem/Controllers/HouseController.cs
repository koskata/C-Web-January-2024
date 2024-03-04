using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contacts.Agent;
using HouseRentingSystem.Core.Contacts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HouseRentingSystem.Core.ErrorMessages.ErrorMessages;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(IHouseService _houseService,
                                IAgentService _agentService)
        {
            houseService = _houseService;
            agentService = _agentService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> All()
        {


            return View(new AllHousesQueryModel());
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }




        [Authorize]
        [MustBeAgent]
        public async Task<IActionResult> Add()
        {
            var model = new HouseFormModel()
            {
                Categories = await houseService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [MustBeAgent]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await houseService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryDoesNotExist);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            var agentId = await agentService.GetAgentId(User.Id());

            model.AgentId = agentId;

            var newHouseId = await houseService.CreateHouseAsync(model);

            return RedirectToAction(nameof(Details), new { Id = newHouseId });
        }






        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseFormModel model, int id)
        {
            return RedirectToAction(nameof(Details), new { Id = "1" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseFormModel model)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
