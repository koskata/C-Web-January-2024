using System.ComponentModel.DataAnnotations;

using static SeminarHub.Data.DataConstants;
using static SeminarHub.Data.ErrorMessages;

namespace SeminarHub.Models
{
    public class SeminarViewModel
    {
        /// <summary>
        /// Seminar Identifier
        /// </summary>
        public int Id { get; set; }

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
        /// Seminar Category
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Date and Time of Seminar
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string DateAndTime { get; set; } = string.Empty;

        /// <summary>
        /// Seminar Organizer
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Organizer { get; set; } = string.Empty;

        
    }
}
