using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForumApp.Core.Contracts;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
using ForumApp.Models;

using Microsoft.EntityFrameworkCore;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private ForumAppDbContext context;
        public PostService(ForumAppDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(PostViewModel model)
        {
            var modelToAdd = new Post()
            {
                Title = model.Title,
                Content = model.Content
            };

            await context.Posts.AddAsync(modelToAdd);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Post? post = await context.Posts.FindAsync(id);

            if (post == null)
            {
                throw new ArgumentException("No post with that id!");
            }

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(PostViewModel model)
        {
            Post? post = await context.Posts.FindAsync(model.Id);

            post.Title = model.Title;
            post.Content = model.Content;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetAllAsync()
        {
            var all = await context.Posts
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                })
                .ToListAsync();

            return all;
        }
    }
}
