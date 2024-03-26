using HouseRentingSystem.Core.Contacts.Statistic;
using HouseRentingSystem.Core.Models.Statistic;

using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers.Api
{
    [ApiController]
    [Route("api/statistic")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticService;

        public StatisticsApiController(IStatisticsService _statisticService)
        {
            statisticService = _statisticService;
        }

        [HttpGet]
        public StatisticServiceModel GetStatistic()
        {
            return statisticService.Total();
        }
    }
}
