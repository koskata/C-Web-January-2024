using ForumApp.Infrastructure.Constants;
using ForumApp.Infrastructure.ErrorMessages;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using ForumApp.Infrastructure.Constants;

namespace ForumApp.Models
{
    public class PostViewModel
    {
        /// <summary>
        /// Post Identifier
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Post Title
        /// </summary>
        [Required]
        [MinLength(ValidationConstants.MinLengthTitle)]
        [StringLength(ValidationConstants.MaxLengthTitle, ErrorMessage = ValidationErrors.InvalidTitle)]
        public string Title { get; set; } = null!;

        /// <summary>
        /// Post Content
        /// </summary>
        [Required]
        [MinLength(ValidationConstants.MinLengthContent)]
        [StringLength(ValidationConstants.MaxLengthContent, ErrorMessage = ValidationErrors.InvalidContent)]
        public string Content { get; set; } = null!;
    }
}
