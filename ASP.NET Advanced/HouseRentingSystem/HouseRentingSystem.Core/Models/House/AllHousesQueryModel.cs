﻿using System.ComponentModel.DataAnnotations;

using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Services.House.Models;

namespace HouseRentingSystem.Core.Models.House
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string Category { get; set; } = null!;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; } = null!;

        public HouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}
