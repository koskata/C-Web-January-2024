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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            var data = new SeedDb();

            builder.HasData(new Agent[] { data.Agent });
        }
    }
}
