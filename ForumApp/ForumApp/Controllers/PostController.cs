using ForumApp.Core.Contracts;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using ForumApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private ForumAppDbContext context;

        public PostController(IPostService _postService, ForumAppDbContext _context)
        {
            postService = _postService;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var all = await postService.GetAllAsync();

            return View(all);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                RedirectToAction(nameof(Index));
            }
            await postService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Post? modelToFind = await context.Posts.FindAsync(id);

            if (modelToFind == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(new PostViewModel()
            {
                Title = modelToFind.Title,
                Content = modelToFind.Content
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                RedirectToAction(nameof(Index));
            }

            await postService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
