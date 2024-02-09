using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data;
using TaskBoardApp.Models;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private ApplicationDbContext context;
        public BoardController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var all = await context
                    .Boards
                    .Select(b => new BoardViewModel()
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Tasks = b.Tasks.Select(t => new TaskViewModel()
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        })
                    }).ToListAsync();

            return View(all);
        }




    }
}
