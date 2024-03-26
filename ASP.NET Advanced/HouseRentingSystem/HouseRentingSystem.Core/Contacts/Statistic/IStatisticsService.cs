using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HouseRentingSystem.Core.Models.Statistic;

namespace HouseRentingSystem.Core.Contacts.Statistic
{
    public interface IStatisticsService
    {
        StatisticServiceModel Total();
    }
}
