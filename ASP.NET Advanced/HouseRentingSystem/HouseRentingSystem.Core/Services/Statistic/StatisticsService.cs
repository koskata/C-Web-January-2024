using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Contacts.Statistic;
using HouseRentingSystem.Core.Models.Statistic;
using HouseRentingSystem.Data;

namespace HouseRentingSystem.Core.Services.Statistic
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HouseRentingSystemDbContext context;

        public StatisticsService(HouseRentingSystemDbContext _context)
        {
            context = _context;
        }

        public StatisticServiceModel Total()
        {
            var totalHouses = context.Houses.Count();
            var totalRents = context.Houses
                .Where(x => x.RenterId != null).Count();

            return new StatisticServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRents = totalRents
            };
        }
    }
}
