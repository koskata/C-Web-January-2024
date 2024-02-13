using System.Security.Claims;
using System.Xml.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NuGet.Protocol.Plugins;

using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;

using static SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Controllers
{
    public class AdController : Controller
    {
        private readonly BazarDbContext context;
        public AdController(BazarDbContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> All()
        {
            var all = await context.Ads
                .AsNoTracking()
                .Select(x => new AdViewModel
                (
                    x.Id,
                    x.Name,
                    x.ImageUrl,
                    x.CreatedOn,
                    x.Category.Name,
                    x.Description,
                    x.Price,
                    x.Owner.UserName
                    )).ToListAsync();

            return View(all);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var modelToAdd = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                OwnerId = GetUserId(),
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.Now,
                CategoryId = model.CategoryId
            };

            await context.Ads.AddAsync(modelToAdd);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string ownerId = GetUserId();

            var ads = await context.AdBuyers
                .Where(x => x.BuyerId == ownerId)
                .Select(x => new AdViewModel(x.Ad.Id, x.Ad.Name,
                x.Ad.ImageUrl, x.Ad.CreatedOn, x.Ad.Category.Name,
                x.Ad.Description, x.Ad.Price, x.Ad.Owner.UserName)).ToListAsync();

            return View(ads);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var model = await context.Ads.FindAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            string currUserId = GetUserId();

            var entry = new AdBuyer()
            {
                AdId = model.Id,
                BuyerId = currUserId,
            };

            if (await context.AdBuyers.ContainsAsync(entry))
            {
                return RedirectToAction(nameof(All));
            }

            await context.AdBuyers.AddAsync(entry);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var model = await context.Ads.FindAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            string currUserId = GetUserId();

            var entry = await context.AdBuyers.Where(x => x.BuyerId == currUserId).FirstOrDefaultAsync();

            if (entry == null)
            {
                return BadRequest();
            }

            context.AdBuyers.Remove(entry);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Ads
                .Where(x => x.Id == id)
                .Select(x => new AdFormViewModel()
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            model.Categories = await GetCategories();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdFormViewModel model, int id)
        {
            var modelToEdit = await context.Ads.FindAsync(id);

            if (modelToEdit == null)
            {
                return BadRequest();
            }

            string currUserId = GetUserId();

            if (currUserId != modelToEdit.OwnerId)
            {
                return Unauthorized();
            }

            if (!(await GetCategories()).Any(x => x.Id == modelToEdit.CategoryId))
            {
                ModelState.AddModelError(nameof(modelToEdit.CategoryId), "Category does not exist!");
            }

            modelToEdit.Name = model.Name;
            modelToEdit.Description = model.Description;
            modelToEdit.ImageUrl = model.ImageUrl;
            modelToEdit.Price = model.Price;
            modelToEdit.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<List<CategoryViewModel>> GetCategories()
            => await context.Categories.Select(x => new CategoryViewModel { Id = x.Id, Name = x.Name }).ToListAsync();
    }
}
