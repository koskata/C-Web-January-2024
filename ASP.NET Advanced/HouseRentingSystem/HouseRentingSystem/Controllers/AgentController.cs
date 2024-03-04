using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contacts.Agent;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Extensions;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static HouseRentingSystem.Core.ErrorMessages.ErrorMessages;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService _agentService)
        {
            agentService = _agentService;
        }

        [Authorize]
        [HttpGet]
        [NotAnAgent]
        public async Task<IActionResult> Become()
        {

            var model = new BecomeAgentFormModel();

            return View(model);
        }

        [HttpPost]
        [NotAnAgent]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var userId = User.Id();

            if (await agentService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), UserWithSamePhoneNumber);
            }

            if (await agentService.UserHasRentsAsync(userId))
            {
                ModelState.AddModelError("Error", UserHasRents);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await agentService.CreateAsync(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
