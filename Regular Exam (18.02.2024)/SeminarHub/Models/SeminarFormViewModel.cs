using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using static SeminarHub.Data.DataConstants;
using static SeminarHub.Data.ErrorMessages;

namespace SeminarHub.Models
{
    public class SeminarFormViewModel
    {
        /// <summary>
        /// Seminar Topic
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarTopicMaxLength, MinimumLength = SeminarTopicMinLength, ErrorMessage = LengthErrorMessage)]
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Seminar Lecturer
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarLecturerMaxLength, MinimumLength = SeminarLecturerMinLength, ErrorMessage = LengthErrorMessage)]
        public string Lecturer { get; set; } = string.Empty;

        /// <summary>
        /// Seminar Details
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarDetailsMaxLength, MinimumLength = SeminarDetailsMinLength, ErrorMessage = LengthErrorMessage)]
        [DisplayName("More Info")]
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// Date And Time of Seminar
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Date of Seminar")]
        public string DateAndTime { get; set; } = string.Empty;

        /// <summary>
        /// Seminar Duration
        /// </summary>
        [Range(SeminarDurationMinLength, SeminarDurationMaxLength)]
        public int Duration { get; set; }

        /// <summary>
        /// Seminar Category Identifier
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Seminar Collection of Categories
        /// </summary>
        public IList<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
