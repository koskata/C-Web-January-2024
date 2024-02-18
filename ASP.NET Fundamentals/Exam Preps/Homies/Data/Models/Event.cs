using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using static Homies.Data.DataConstants;
using Type = Homies.Data.Models.Type;

using Humanizer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data.Models
{
    public class Event
    {

        [Key]
        [Comment("Event Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(EventNameMaxLength)]
        [Comment("Event Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(EventDescriptionMaxLength)]
        [Comment("Event Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Event Organiser Identifier")]
        public string OrganiserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OrganiserId))]
        [Comment("Event Organiser")]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        [Comment("Event Creation Date")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Event Starting Date")]
        public DateTime Start { get; set; }

        [Required]
        [Comment("Event Ending Date")]
        public DateTime End { get; set; }

        [Required]
        [Comment("Event Type Identifier")]
        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        [Comment("Event Type")]
        public Type Type { get; set; } = null!;

        [Comment("Event Participants")]
        public IList<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
    }

//    •	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 5 and max length 20 (required)
//•	Has Description – a string with min length 15 and max length 150 (required)
//•	Has OrganiserId – an string (required)
//•	Has Organiser – an IdentityUser(required)
//•	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has Start – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has End – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//•	Has TypeId – an integer, foreign key (required)
//•	Has Type – a Type (required)
//•	Has EventsParticipants – a collection of type EventParticipant

    }
