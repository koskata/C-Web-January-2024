using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Category Collection of Seminars")]
        public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
    }
}