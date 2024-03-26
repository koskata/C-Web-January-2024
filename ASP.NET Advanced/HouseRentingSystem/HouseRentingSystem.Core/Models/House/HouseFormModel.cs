using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HouseRentinSystem.Infrastructure.Data.Models;

using Microsoft.EntityFrameworkCore;

using static HouseRentinSystem.Infrastructure.Constants.DataConstants;
using static HouseRentingSystem.Core.ErrorMessages.ErrorMessages;
using HouseRentingSystem.Core.Contacts.House;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseFormModel : IHouseModel
    {
        [Required(ErrorMessage = Required)]
        [StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength, ErrorMessage = HouseTitleLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = Required)]
        [StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength, ErrorMessage = HouseAddressLengthErrorMessage)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = Required)]
        [StringLength(HouseDescriptionMaxLength, MinimumLength = HouseDescriptionMinLength, ErrorMessage = HouseDescriptionLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = Required)]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), HousePricePerMonthMinLength, HousePricePerMonthMaxLength, ConvertValueInInvariantCulture = true)]
        public decimal PricePerMonth { get; set; }

        [Required(ErrorMessage = Required)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = Required)]
        public int AgentId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
    }
}
