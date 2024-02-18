using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {
        [Required]
        [Comment("SeminarParticipant Seminar Identifier")]
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
        [Comment("SeminarParticipant Seminar")]
        public Seminar Seminar { get; set; }

        [Required]
        [Comment("SeminarParticipant Participant Identifier")]
        public string ParticipantId { get; set; } = string.Empty;


        [ForeignKey(nameof(ParticipantId))]
        [Comment("SeminarParticipant Participant")]
        public IdentityUser Participant { get; set; }
    }
}