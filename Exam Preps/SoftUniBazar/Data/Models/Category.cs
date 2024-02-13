using Humanizer;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

using static SoftUniBazar.Data.DataConstants;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public string Name { get; set; }

        [Comment("Category Collection of Ads")]
        public IList<Ad> Ads { get; set; } = new List<Ad>();
    }

//    •	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 3 and max length 15 (required)
//•	Has Ads – a collection of type Ad

}