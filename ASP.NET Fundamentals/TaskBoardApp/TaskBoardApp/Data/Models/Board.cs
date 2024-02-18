using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(DataConstants.NameMaxLength)]
        public string Name { get; init; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}