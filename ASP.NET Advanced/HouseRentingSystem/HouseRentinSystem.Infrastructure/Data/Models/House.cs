using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using static HouseRentinSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentinSystem.Infrastructure.Data.Models
{
    public class House
    {
        [Key]
        [Comment("House Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(HouseTitleMaxLength)]
        [Comment("House Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseAddressMaxLength)]
        [Comment("House Address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(HouseDescriptionMaxLength)]
        [Comment("House Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("House ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), HousePricePerMonthMinLength, HousePricePerMonthMaxLength, ConvertValueInInvariantCulture = true)]
        [Comment("House Monthly Price")]
        public decimal PricePerMonth { get; set; }

        [Required]
        [Comment("House Category Identifier")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [Comment("House Category")]
        public Category Category { get; set; } = null!;

        [Required]
        [Comment("House Agent Identifier")]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        [Comment("House Agent")]
        public Agent Agent { get; set; } = null!;

        [Comment("House Renter Identifier")]
        public string? RenterId { get; set; }
    }

}