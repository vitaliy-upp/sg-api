using NoLimitTech.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [PasswordValidation(RequiredLength = 6, RequireLowercase = false, RequireUppercase = false, RequireNonAlphanumeric = false, RequireDigit = false)]
        public string OldPassword { get; set; }

        [Required]
        [PasswordValidation]
        public string NewPassword { get; set; }
    }
}
