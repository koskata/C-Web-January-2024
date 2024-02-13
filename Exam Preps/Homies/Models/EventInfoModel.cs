using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.DataConstants;

namespace Homies.Models
{
    public class EventInfoModel
    {
        public EventInfoModel(int id, string name, string organiser, DateTime start, string type)
        {
            Id = id;
            Name = name;
            Organiser = organiser;
            Start = start.ToString(DateFormat);
            Type = type;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EventNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Organiser { get; set; } = string.Empty;

        [Required]
        public string Start { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
