using Microsoft.VisualBasic;

namespace SeminarHub.Data
{
    public static class ErrorMessages
    {
        public const string RequiredErrorMessage = "The field {0} is required!";
        public const string LengthErrorMessage = "The length of the field must be between {2} and {1} characters long.";

        

        public const string DateTimeValidation = $"Invalid Date. Format must be {DataConstants.DateFormat}";
    }
}
