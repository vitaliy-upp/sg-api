using Domain.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
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
