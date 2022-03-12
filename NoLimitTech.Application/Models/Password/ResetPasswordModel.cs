using NoLimitTech.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string ResetToken { get; set; }
        
        [Required]
        [PasswordValidation]
        public string NewPassword { get; set; }
    }
}
