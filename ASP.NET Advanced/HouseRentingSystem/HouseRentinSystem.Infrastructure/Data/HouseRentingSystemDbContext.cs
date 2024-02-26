using HouseRentinSystem.Infrastructure.Data.Models;
using HouseRentinSystem.Infrastructure.Data.SeedDb;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Data
{
    public class HouseRentingSystemDbContext : IdentityDbContext
    {
        public HouseRentingSystemDbContext(DbContextOptions<HouseRentingSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<House> Houses { get; set; }

        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new HouseConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(builder);
        }
    }
}