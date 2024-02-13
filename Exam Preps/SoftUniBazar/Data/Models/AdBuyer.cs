using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SoftUniBazar.Data.Models
{
    public class AdBuyer
    {
        [Required]
        [Comment("AdBuyer Buyer Identifier")]
        public string BuyerId { get; set; } = string.Empty;

        [Comment("AdBuyer Buyer")]
        public IdentityUser Buyer { get; set; }

        [Required]
        [Comment("AdBuyer Ad Identifier")]
        public int AdId { get; set; }

        [Comment("AdBuyer Ad")]
        public Ad Ad { get; set; }
    }
}
