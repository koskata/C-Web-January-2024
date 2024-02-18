using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForumApp.Infrastructure.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace ForumApp.Infrastructure.Data
{
    public class ForumAppDbContext : DbContext
    {
        public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasData(
                new Post() { Id = 1, Title = "My first post", Content = "My first post will be about CRUD operations in MVC. It's so much fun!" },
                new Post() { Id = 2, Title = "My second post", Content = "This is my second post. CRUD operations are getting more and more interesting!" },
                new Post() { Id = 3, Title = "My third post", Content = "Hello there! I'm getting better and better with the CRUD operations in MVC. Stay tuned!" }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Post> Posts { get; set; }
    }
}
