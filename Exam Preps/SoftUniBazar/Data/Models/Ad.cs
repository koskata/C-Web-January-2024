using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using static SoftUniBazar.Data.DataConstants;

using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {
        [Key]
        [Comment("Ad Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(AdNameMaxLength)]
        [Comment("Ad Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(AdDescriptionMaxLength)]
        [Comment("Ad Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Ad Price")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Ad Owner Identifier")]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [Comment("Ad Owner")]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [Comment("Ad Image Url")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Ad Date of Creation")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Ad Category Identifier")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        [Comment("Ad Category")]
        public Category Category { get; set; } = null!;
    }

}
