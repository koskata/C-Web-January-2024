using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Contacts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Data;

using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingSystemDbContext context;

        public HouseService(HouseRentingSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
            => await context.Categories.Select(x => new HouseCategoryServiceModel { Id = x.Id, Name = x.Name }).ToListAsync();

        public async Task<bool> CategoryExistsAsync(int categoryId)
            => await context.Categories.AnyAsync(x => x.Id == categoryId);

        public async Task<int> CreateHouseAsync(HouseFormModel model)
        {
            var house = new HouseRentinSystem.Infrastructure.Data.Models.House()
            {
                Title = model.Title,
                Description = model.Description,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                PricePerMonth = model.PricePerMonth,
                CategoryId = model.CategoryId,
                AgentId = model.AgentId
            };

            await context.Houses.AddAsync(house);
            await context.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        {
            var lastThreeHouses = await context.Houses
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    ImageUrl = h.ImageUrl
                })
                .Take(3)
                .ToListAsync();

            return lastThreeHouses;
        }


    }
}
