using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Contacts.House;
using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Core.Services.House.Models;
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

        public HouseQueryServiceModel All(string category = null,
                                            string searchTerm = null,
                                            HouseSorting sorting = HouseSorting.Newest,
                                            int currentPage = 1,
                                            int housesPerPage = 1)
        {
            var housesQuery = context.Houses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = context.Houses.Where(x => x.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = housesQuery.Where(x =>
                                                    x.Title.ToLower().Contains(searchTerm.ToLower()) ||
                                                    x.Address.ToLower().Contains(searchTerm.ToLower()) ||
                                                    x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery.OrderBy(x => x.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                                .OrderBy(x => x.RenterId != null)
                                .ThenByDescending(x => x.Id),
                _ => housesQuery.OrderByDescending(x => x.Id)
            };

            var houses = housesQuery
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(x => new HouseServiceModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    ImageUrl = x.ImageUrl,
                    IsRented = x.RenterId != null,
                    PricePerMonth = x.PricePerMonth,
                }).ToList();

            var totalHouses = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHouses,
                Houses = houses
            };
        }


        public async Task<IEnumerable<string>> AllCategoriesNames()
            => await context.Categories.Select(x => x.Name).Distinct().ToListAsync();



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

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId)
        {
            var houses = await context
                            .Houses
                            .Where(x => x.AgentId == agentId)
                            .ToListAsync();

            return ProjectToModel(houses);
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
        {
            var houses = await context
                           .Houses
                           .Where(x => x.RenterId == userId)
                           .ToListAsync();

            return ProjectToModel(houses);
        }


        private List<HouseServiceModel> ProjectToModel(List<HouseRentinSystem.Infrastructure.Data.Models.House> houses)
        {
            var result = houses
                .Select(x => new HouseServiceModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    ImageUrl = x.ImageUrl,
                    PricePerMonth = x.PricePerMonth,
                    IsRented = x.RenterId != null
                }).ToList();

            return result;
        }

        public async Task<bool> Exists(int id)
            => await context.Houses.AnyAsync(x => x.Id == id);

        public async Task<HouseDetailsServiceModel> HouseDetailsById(int id)
        {
            return await context.Houses
                .Where(x => x.Id == id)
                .Select(x => new HouseDetailsServiceModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Address = x.Address,
                    ImageUrl = x.ImageUrl,
                    PricePerMonth = x.PricePerMonth,
                    Description = x.Description,
                    IsRented = x.RenterId != null,
                    Category = x.Category.Name,
                    Agent = new AgentServiceModel()
                    {
                        PhoneNumber = x.Agent.PhoneNumber,
                        Email = x.Agent.User.Email
                    }
                }).FirstAsync();
        }

        public async Task Edit(HouseFormModel model, int id)
        {
            var house = await context.Houses.FindAsync(id);

            if (house != null)
            {
                house.Title = model.Title;
                house.Address = model.Address;
                house.ImageUrl = model.ImageUrl;
                house.Description = model.Description;
                house.PricePerMonth = model.PricePerMonth;
                house.CategoryId = model.CategoryId;

                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
        {
            return await context.Houses.AnyAsync(h => h.Id == houseId && h.Agent.UserId == currentUserId);
        }

        public async Task<HouseFormModel> GetHouseFormModelByIdAsync(int houseId)
        {
            var house = await context.Houses
                .Where(h => h.Id == houseId)
                .Select(h => new HouseFormModel()
                {
                    Address = h.Address,
                    CategoryId = h.CategoryId,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    Title = h.Title
                })
                .FirstOrDefaultAsync();

            if (house != null)
            {
                house.Categories = await AllCategoriesAsync();
            }

            return house;
        }

        public async Task Delete(int houseId)
        {
            var house = await context.Houses.FindAsync(houseId);

            context.Remove(house);

            await context.SaveChangesAsync();
        }

        public async Task<bool> IsRentedAsync(int id)
        {
            bool result = false;

            var house = await context.Houses.FirstOrDefaultAsync(x => x.Id == id);

            if (house != null)
            {
                if (house.RenterId != null)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task<bool> IsRentedByUserWithIdAsync(int houseId, string userId)
        {
            bool result = false;

            var house = await context.Houses.FirstOrDefaultAsync(x => x.Id == houseId);

            if (house != null)
            {
                if (house.RenterId == userId)
                {
                    result = true;
                }
            }

            return result;
        }

        public async Task RentAsync(int houseId, string userId)
        {
            var house = await context.Houses.FindAsync(houseId);

            house.RenterId = userId;
            await context.SaveChangesAsync();
        }

        public async Task Leave(int houseId)
        {
            var house = await context.Houses.FindAsync(houseId);

            house.RenterId = null;
            await context.SaveChangesAsync();
        }
    }
}
