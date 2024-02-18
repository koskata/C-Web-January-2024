using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Humanizer;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        [Comment("Task Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.TitleMaxLength)]
        [Comment("Task Title")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DataConstants.DescriptionMaxLength)]
        [Comment("Task Description")]
        public string Description { get; set; } = null!;

        [Comment("Task Date of Creation")]
        public DateTime CreatedOn { get; set; }

        [Comment("Task Board Identifier")]
        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        [Comment("Task Board")]
        public Board? Board { get; set; }

        [Required]
        [Comment("Task Owner Identifier")]
        public string OwnerId { get; set; } = null!;

        [Comment("Task Owner")]
        public IdentityUser Owner { get; set; } = null!;
    }

    //•	Id – a unique integer, Primary Key
    //•	Title – a string with min length 5 and max length 70 (required)
    //•	Description – a string with min length 10 and max length 1000 (required)
    //•	CreatedOn – date and time
    //•	BoardId – an integer
    //•	Board – a Board object
    //•	OwnerId – an integer(required)
    //•	Owner – an IdentityUser object

}
