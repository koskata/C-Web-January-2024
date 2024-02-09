using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TaskBoardApp.Data;
using TaskBoardApp.Models;

using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext context;

        public TaskController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new TaskFormModel()
            {
                Boards = await GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!(await GetBoards()).Any(x => x.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            string currentUserId = GetUserId();

            //if (!ModelState.IsValid)
            //{
            //    model.Boards = await GetBoards();
            //
            //    return View(model);
            //}

            Task task = new Task()
            {
                Title = model.Title,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                BoardId = model.BoardId,
                OwnerId = currentUserId
            };

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            var boards = context.Boards;

            return RedirectToAction("Index", "Board");
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = await context
                .Tasks
                .Where(x => x.Id == id)
                .Select(x => new TaskDetailsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = x.Board.Name,
                    Owner = x.Owner.UserName
                }).FirstOrDefaultAsync();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskFormModel taskModel = new()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await GetBoards()
            };

            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel model)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            if (!(await GetBoards()).Any(x => x.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
            }

            //if (!ModelState.IsValid)
            //{
            //    model.Boards = await GetBoards();

            //    return View(model);
            //}

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }


            TaskViewModel taskModel = new()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(taskModel);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel taskViewModel)
        {
            var task = await context.Tasks.FindAsync(taskViewModel.Id);

            if (task == null)
            {
                return BadRequest();
            }

            string currentUserId = GetUserId();
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            context.Tasks.Remove(task);

            await context.SaveChangesAsync();

            return RedirectToAction("Index", "Board");
        }


        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        {
            return context.Boards.Select(x => new TaskBoardModel
            {
                Id = x.Id,
                Name = x.Name,
            });
        }
    }
}
