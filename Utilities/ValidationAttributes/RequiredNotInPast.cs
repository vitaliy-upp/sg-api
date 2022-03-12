using System;
using System.ComponentModel.DataAnnotations;

namespace Utilities.ValidationAttributes
{
    public class RequiredNotInPast : ValidationAttribute
    {
        /// <summary>
        /// Validate that date not in past
        /// </summary>
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                var utcDt = dateTime.ToUniversalTime();
                return utcDt >= DateTime.UtcNow;
            }
            return false;
        }
    }
}
