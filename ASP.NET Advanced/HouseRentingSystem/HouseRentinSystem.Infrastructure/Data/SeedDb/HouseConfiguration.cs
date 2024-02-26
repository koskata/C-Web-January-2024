using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentinSystem.Infrastructure.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseRentinSystem.Infrastructure.Data.SeedDb
{
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder
                .HasOne(a => a.Category)
                .WithMany(a => a.Houses)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.Agent)
                .WithMany()
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);


            var data = new SeedDb();

            builder.HasData(new House[] { data.ThirdHouse, data.FirstHouse, data.SecondHouse });
        }
    }
}
