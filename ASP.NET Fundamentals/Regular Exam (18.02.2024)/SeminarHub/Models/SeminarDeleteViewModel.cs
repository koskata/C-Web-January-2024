using System.ComponentModel.DataAnnotations;

using static SeminarHub.Data.DataConstants;
using static SeminarHub.Data.ErrorMessages;

namespace SeminarHub.Models
{
    public class SeminarDeleteViewModel
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
        /// Date And Time of Seminar
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public DateTime DateAndTime { get; set; }

        /// <summary>
        /// Seminar Organizer Identifier
        /// </summary>
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string OrganizerId { get; set; } = string.Empty;
    }
}
