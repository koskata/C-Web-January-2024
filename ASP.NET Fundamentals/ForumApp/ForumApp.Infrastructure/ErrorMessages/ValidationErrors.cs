using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp.Infrastructure.ErrorMessages
{
    public class ValidationErrors
    {
        public const string InvalidTitle = "Field Title must be in range 10-50";
        public const string InvalidContent = "Field Content must be in range 30-1500";
    }
}
