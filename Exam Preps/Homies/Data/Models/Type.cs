using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static Homies.Data.DataConstants;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        [Comment("Type Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(TypeNameMaxLength)]
        [Comment("Type Name")]
        public string Name { get; set; }

        [Comment("Type Events")]
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
