using System.ComponentModel.DataAnnotations;

using TaskBoardApp.Data;

namespace TaskBoardApp.Models
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(DataConstants.TitleMaxLength, MinimumLength = DataConstants.TitleMinLength, ErrorMessage = "Title must be at least {2} chars long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DataConstants.DescriptionMaxLength, MinimumLength = DataConstants.DescriptionMinLength, ErrorMessage = "Description must be at least {2} chars long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "BoardId")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; }
    }
}
