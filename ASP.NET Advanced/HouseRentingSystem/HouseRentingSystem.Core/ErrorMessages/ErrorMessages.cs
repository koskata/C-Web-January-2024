using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.ErrorMessages
{
    public static class ErrorMessages
    {
        public const string AgentPhoneLengthErrorMessage = "Agent Phone Number must be between {2} and {1} characters long.";
        public const string HouseTitleLengthErrorMessage = "Title must be between {2} and {1} characters long.";
        public const string HouseAddressLengthErrorMessage = "Address must be between {2} and {1} characters long.";
        public const string HouseDescriptionLengthErrorMessage = "Description must be between {2} and {1} characters long.";
        public const string Required = "The field {0} is required";
        




        //--------------------------------------------- ModelState

        public const string UserWithSamePhoneNumber = "Agent with same phone number exists already!";
        public const string UserHasRents = "You should have no rents to become an agent!";
        public const string CategoryDoesNotExist = "Category does not exist!";
    }
}
