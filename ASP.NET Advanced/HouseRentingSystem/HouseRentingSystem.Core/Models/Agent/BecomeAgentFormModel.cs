using System.ComponentModel.DataAnnotations;

using static HouseRentinSystem.Infrastructure.Constants.DataConstants;
using static HouseRentingSystem.Core.ErrorMessages.ErrorMessages;

namespace HouseRentingSystem.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = Required)]
        [StringLength(AgentPhoneNumberMaxLength, MinimumLength = AgentPhoneNumberMinLength, ErrorMessage = AgentPhoneLengthErrorMessage)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
