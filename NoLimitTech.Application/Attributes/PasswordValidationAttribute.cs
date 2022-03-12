using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NoLimitTech.Application.Attributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireDigit { get; set; }


        public PasswordValidationAttribute(
            int RequiredLength = 8
            , bool RequireLowercase = true
            , bool RequireUppercase = true
            , bool RequireNonAlphanumeric = true
            , bool RequireDigit = true)
        {
            this.RequiredLength = RequiredLength;
            this.RequireLowercase = RequireLowercase;
            this.RequireUppercase = RequireUppercase;
            this.RequireNonAlphanumeric = RequireNonAlphanumeric;
            this.RequireDigit = RequireDigit;
        }

        public override bool IsValid(object value)
        {
            string pwd = value.ToString();

            if (RequiredLength > pwd.Length)
            { return false; }

            if (RequireLowercase && !new Regex(@"[a-z]").IsMatch(pwd))
            { return false; }

            if (RequireUppercase && !new Regex(@"[A-Z]").IsMatch(pwd))
            { return false; }

            if (RequireNonAlphanumeric && !new Regex(@"\W|_").IsMatch(pwd))
            { return false; }

            if(RequireDigit && !new Regex(@"[\d]").IsMatch(pwd))
            { return false; }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }
    }
}
