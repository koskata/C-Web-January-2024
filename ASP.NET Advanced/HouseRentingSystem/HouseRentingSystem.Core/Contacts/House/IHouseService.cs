using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Core.Services.House.Models;

namespace HouseRentingSystem.Core.Contacts.House
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();

        Task<int> CreateHouseAsync(HouseFormModel model);

        Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<IEnumerable<string>> AllCategoriesNames();

        HouseQueryServiceModel All(string category = null,
                                    string searchTerm = null,
                                    HouseSorting sorting = HouseSorting.Newest,
                                    int currentPage = 1,
                                    int housesPerPage = 1);

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId);

        Task<bool> Exists(int id);

        Task<HouseDetailsServiceModel> HouseDetailsById(int id);

        Task Edit(HouseFormModel model, int id);

        Task<bool> HasAgentWithId(int houseId, string currentUserId);

        Task<HouseFormModel> GetHouseFormModelByIdAsync(int houseId);

        Task Delete(int houseId);

        Task<bool> IsRentedAsync(int id);

        Task<bool> IsRentedByUserWithIdAsync(int houseId, string userId);

        Task RentAsync(int houseId, string userId);

        Task Leave(int houseId);
    }
}
