using System.Globalization;
using System.Security.Claims;

using Homies.Data;
using Homies.Data.Models;
using Homies.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static Homies.Data.DataConstants;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;

        public EventController(HomiesDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IActionResult> All()
        {
            var all = await context.Events
                .AsNoTracking()
                .Select(x => new EventInfoModel
                (
                    x.Id,
                    x.Name,
                    x.Organiser.UserName,
                    x.Start,
                    x.Type.Name
                )).ToListAsync();

            return View(all);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormModel();
            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormModel model)
        {

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(model.Start, $"Invalid Date. Format must be {DateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(model.End, $"Invalid Date. Format must be {DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();

                return View(model);
            }

            var modelToAdd = new Event()
            {
                CreatedOn = DateTime.Now,
                Name = model.Name,
                Description = model.Description,
                OrganiserId = GetId(),
                Start = start,
                End = end,
                TypeId = model.TypeId,
            };

            await context.Events.AddAsync(modelToAdd);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string organiserId = GetId();

            var model = await context.EventParticipants
                .Where(x => x.HelperId == organiserId)
                .AsNoTracking()
                .Select(x => new EventInfoModel(x.EventId, x.Event.Name, x.Event.Organiser.UserName, x.Event.Start, x.Event.Type.Name))
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var model = await context.Events
                .Where(x => x.Id == id)
                .Include(x => x.EventsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string organiserId = GetId();

            if (!model.EventsParticipants.Any(x => x.HelperId == organiserId))
            {
                model.EventsParticipants.Add(new EventParticipant()
                {
                    HelperId = organiserId,
                    EventId = model.Id,
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }


        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var model = await context.Events
                .Where(x => x.Id == id)
                .Include(x => x.EventsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string organiserId = GetId();

            var ep = await context.EventParticipants.FirstOrDefaultAsync(x => x.HelperId == organiserId);

            if (ep == null)
            {
                return BadRequest();
            }

            model.EventsParticipants.Remove(ep);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var modelToFind = await context.Events.FindAsync(id);

            var model = await context.Events
                .Where(x => x.Id == id)
                .Select(x => new EventFormModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Start = x.Start.ToString(DateFormat),
                    End = x.End.ToString(DateFormat),
                    TypeId = x.TypeId,
                })
                .FirstOrDefaultAsync();

            model.Types = await GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormModel model, int id)
        {
            var e = await context.Events.FindAsync(id);

            if (e == null)
            {
                return BadRequest();
            }


            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(model.Start, $"Invalid Date. Format must be {DateFormat}");
            }

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(model.End, $"Invalid Date. Format must be {DateFormat}");
            }

            e.Name = model.Name;
            e.Description = model.Description;
            e.Start = start;
            e.End = end;
            e.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Events
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new EventDetailsModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Start = x.Start.ToString(DateFormat),
                    End = x.End.ToString(DateFormat),
                    Organiser = x.Organiser.UserName,
                    CreatedOn = x.CreatedOn.ToString(DateFormat),
                    Type = x.Type.Name
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }





        private string GetId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<List<TypeViewModel>> GetTypes()
            => await context.Types
            .Select(x => new TypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
    }
}
