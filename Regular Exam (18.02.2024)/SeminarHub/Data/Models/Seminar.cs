using Humanizer;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;

using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        [Comment("Seminar Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(SeminarTopicMaxLength)]
        [Comment("Seminar Topic")]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarLecturerMaxLength)]
        [Comment("Seminar Lecturer")]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [StringLength(SeminarDetailsMaxLength)]
        [Comment("Seminar Details")]
        public string Details { get; set; } = string.Empty;

        [Required]
        [Comment("Seminar Organizer Identifier")]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        [Comment("Seminar Organizer")]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [Comment("Date and Time of Seminar")]
        public DateTime DateAndTime { get; set; }

        [Range(SeminarDurationMinLength, SeminarDurationMaxLength)]
        [Comment("Seminar Duration")]
        public int Duration { get; set; }

        [Required]
        [Comment("Seminar Category Identifier")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        [Comment("Seminar Category")]
        public Category Category { get; set; } = null!;

        [Comment("Seminar Collection of SeminarsParticipants")]
        public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }

}
