﻿using System.ComponentModel;

using HouseRentingSystem.Core.Contacts.House;

namespace HouseRentingSystem.Core.Services.House.Models
{
    public class HouseServiceModel : IHouseModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; } = null!;

        [DisplayName("Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [DisplayName("Is Rented")]
        public bool IsRented { get; set; }
    }
}