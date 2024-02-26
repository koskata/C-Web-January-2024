using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static HouseRentinSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentinSystem.Infrastructure.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Category Collection of Houses")]
        public IList<House> Houses { get; set; } = new List<House>();
    }
}
