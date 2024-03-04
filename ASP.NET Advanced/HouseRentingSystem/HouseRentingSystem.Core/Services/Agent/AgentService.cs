using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Contacts.Agent;
using HouseRentingSystem.Data;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.Agent
{

    public class AgentService : IAgentService
    {
        private readonly HouseRentingSystemDbContext context;

        public AgentService(HouseRentingSystemDbContext _context)
        {
            context = _context;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            var agent = new HouseRentinSystem.Infrastructure.Data.Models.Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await context.Agents.AddAsync(agent);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistByIdAsync(string userId)
            => await context.Agents.AnyAsync(x => x.UserId == userId);

        public async Task<int> GetAgentId(string userId)
            => (await context.Agents.FirstOrDefaultAsync(x => x.UserId == userId)).Id;

        public async Task<bool> UserHasRentsAsync(string userId)
            => await context.Houses.AnyAsync(x => x.RenterId == userId);

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
            => await context.Agents.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }
}
