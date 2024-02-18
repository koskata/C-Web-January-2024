using System.ComponentModel.DataAnnotations;

using static SeminarHub.Data.DataConstants;
using static SeminarHub.Data.ErrorMessages;

namespace SeminarHub.Models
{
    public class CategoryViewModel
    {
        /// <summary>
        /// Category Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}