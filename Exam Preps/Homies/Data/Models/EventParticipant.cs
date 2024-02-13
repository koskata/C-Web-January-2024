using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data.Models
{
    public class EventParticipant
    {
        [Comment("Event Participants Helper Identifier")]
        public string HelperId { get; set; }

        [ForeignKey(nameof(HelperId))]
        [Comment("Event Participants Helper")]
        public IdentityUser Helper { get; set; }

        [Comment("Event Participants Event Identifier")]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        [Comment("Event Participants Event")]
        public Event Event { get; set; }
    }
}