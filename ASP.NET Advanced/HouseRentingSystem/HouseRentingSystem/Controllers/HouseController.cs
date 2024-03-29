﻿using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contacts.Agent;
using HouseRentingSystem.Core.Contacts.House;
using HouseRentingSystem.Core.Extensions;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Core.Services.House.Models;
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
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            var houseCategories = await houseService.AllCategoriesNames();
            query.Categories = houseCategories;

            return View(query);
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses = null;

            var userId = User.Id();

            if (await agentService.ExistByIdAsync(userId))
            {
                var currentAgentId = await agentService.GetAgentId(userId);

                myHouses = await houseService.AllHousesByAgentId(currentAgentId);
            }
            else
            {
                myHouses = await houseService.AllHousesByUserId(userId);
            }

            return View(myHouses);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            var model = await houseService.HouseDetailsById(id);

            if (information != model.GetInformation())
            {
                return BadRequest();
            }

            return View(model);
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

            return RedirectToAction(nameof(Details), new { Id = newHouseId, information = model.GetInformation() });
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            if (await houseService.HasAgentWithId(id, this.User.Id()) == false)
            {
                return Unauthorized();
            }

            var model = await houseService.GetHouseFormModelByIdAsync(id);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseFormModel model, int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return this.View();
            }

            if (await houseService.HasAgentWithId(id, this.User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await houseService.CategoryExistsAsync(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.AllCategoriesAsync();

                return View(model);
            }

            await houseService.Edit(model, id);

            return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await houseService.Exists(id))
            {
                return BadRequest();
            }

            if (!await houseService.HasAgentWithId(id, User.Id()))
            {
                return Unauthorized();
            }

            var house = await houseService.HouseDetailsById(id);

            var model = new HouseDetailsViewModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (!await houseService.Exists(model.Id))
            {
                return BadRequest();
            }

            if (!await houseService.HasAgentWithId(model.Id, User.Id()))
            {
                return Unauthorized();
            }

            await houseService.Delete(model.Id);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await houseService.Exists(id) == false)
            {
                return BadRequest();
            }

            if (!await agentService.ExistByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            if (await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await houseService.RentAsync(id, User.Id());

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (!await houseService.Exists(id) || !await houseService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            if (!await houseService.IsRentedByUserWithIdAsync(id, User.Id()))
            {
                return Unauthorized();
            }

            await houseService.Leave(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
