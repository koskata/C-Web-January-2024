using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using static Homies.Data.DataConstants;

using Microsoft.EntityFrameworkCore;

namespace Homies.Models
{
    public class EventDetailsModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        public string Organiser { get; set; } = null!;

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
