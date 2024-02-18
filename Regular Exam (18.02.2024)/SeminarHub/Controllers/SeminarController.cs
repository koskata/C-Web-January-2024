using System.Globalization;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;

using static SeminarHub.Data.DataConstants;
using static SeminarHub.Data.ErrorMessages;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;
        public SeminarController(SeminarHubDbContext _context)
        {
            context = _context;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel()
            {

            };

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime dateAndTime;

            if (!DateTime.TryParseExact(model.DateAndTime, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), DateTimeValidation);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var modelToAdd = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = GetUserId(),
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId
            };

            await context.AddAsync(modelToAdd);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> All()
        {
            var all = await context.Seminars
                .AsNoTracking()
                .Select(x => new SeminarViewModel()
                {
                    Id = x.Id,
                    Topic = x.Topic,
                    Lecturer = x.Lecturer,
                    Category = x.Category.Name,
                    DateAndTime = x.DateAndTime.ToString(DateFormat),
                    Organizer = x.Organizer.UserName
                })
                .ToListAsync();

            return View(all);
        }


        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var joined = await context.SeminarsParticipants
                            .Where(x => x.ParticipantId == userId)
                            .AsNoTracking()
                            .Select(x => new SeminarViewModel()
                            {
                                Id = x.Seminar.Id,
                                Topic = x.Seminar.Topic,
                                Lecturer = x.Seminar.Lecturer,
                                Category = x.Seminar.Category.Name,
                                DateAndTime = x.Seminar.DateAndTime.ToString(DateFormat),
                                Organizer = x.Seminar.Organizer.UserName
                            }).ToListAsync();

            return View(joined);
        }


        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var model = await context.Seminars
                .Where(x => x.Id == id)
                .Include(x => x.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var entry = new SeminarParticipant()
            {
                SeminarId = id,
                ParticipantId = userId,
            };

            if (model.SeminarsParticipants.Any(x => x.ParticipantId == userId))
            {
                return RedirectToAction(nameof(All));
            }

            model.SeminarsParticipants.Add(entry);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var model = await context.Seminars
                .Where(x => x.Id == id)
                .Include(x => x.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var entry = await context.SeminarsParticipants
                .FirstOrDefaultAsync(x => x.ParticipantId == userId);

            if (entry == null)
            {
                return BadRequest();
            }

            model.SeminarsParticipants.Remove(entry);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Seminars.FindAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != model.OrganizerId)
            {
                return Unauthorized();
            }

            var modelToEdit = new SeminarFormViewModel()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = model.DateAndTime.ToString(DateFormat),
                Duration = model.Duration,
                CategoryId = model.CategoryId
            };

            modelToEdit.Categories = await GetCategories();

            return View(modelToEdit);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            DateTime dateAndTime;

            if (!DateTime.TryParseExact(model.DateAndTime, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
            {
                ModelState.AddModelError(nameof(model.DateAndTime), DateTimeValidation);
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var modelToEdit = await context.Seminars.FindAsync(id);

            if (modelToEdit == null)
            {
                return BadRequest();
            }

            modelToEdit.Topic = model.Topic;
            modelToEdit.Lecturer = model.Lecturer;
            modelToEdit.Details = model.Details;
            modelToEdit.DateAndTime = dateAndTime;
            modelToEdit.Duration = model.Duration;
            modelToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Seminars
                .Where(x => x.Id == id)
                .AsNoTracking()
                .Select(x => new SeminarDetailsViewModel()
                {
                    Id = id,
                    Topic = x.Topic,
                    DateAndTime = x.DateAndTime.ToString(DateFormat),
                    Duration = x.Duration,
                    Lecturer = x.Lecturer,
                    Category = x.Category.Name,
                    Details = x.Details,
                    Organizer = x.Organizer.UserName
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Seminars.FindAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (userId != model.OrganizerId)
            {
                return Unauthorized();
            }

            var modelToDelete = new SeminarDeleteViewModel()
            {
                Id = id,
                Topic = model.Topic,
                DateAndTime = model.DateAndTime
            };

            return View(modelToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDeleteViewModel model)
        {
            string userId = GetUserId();
            var modelToDelete = await context.Seminars.FindAsync(model.Id);

            if (modelToDelete == null)
            {
                return BadRequest();
            }

            if (modelToDelete.OrganizerId != userId)
            {
                return Unauthorized();
            }

            var entry = await context.SeminarsParticipants.FirstOrDefaultAsync(x => x.SeminarId == modelToDelete.Id);

            if (entry != null)
            {
                context.SeminarsParticipants.Remove(entry);
            }

            context.Seminars.Remove(modelToDelete);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }




        public string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);


        public async Task<List<CategoryViewModel>> GetCategories()
            => await context.Categories
                                .Select(x => new CategoryViewModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name
                                }).ToListAsync();
    }
}
