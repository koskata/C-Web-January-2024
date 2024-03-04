﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Contacts.House;
using HouseRentingSystem.Core.Enums;
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
    }
}
