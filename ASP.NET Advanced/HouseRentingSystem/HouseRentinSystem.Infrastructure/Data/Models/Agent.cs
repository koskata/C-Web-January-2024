using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static HouseRentinSystem.Infrastructure.Constants.DataConstants;

namespace HouseRentinSystem.Infrastructure.Data.Models
{
    public class Agent
    {
        [Key]
        [Comment("Agent Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgentPhoneNumberMaxLength)]
        [Comment("Agent Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Comment("Agent User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [Comment("Agent User")]
        public IdentityUser User { get; set; } = null!;
    }


}