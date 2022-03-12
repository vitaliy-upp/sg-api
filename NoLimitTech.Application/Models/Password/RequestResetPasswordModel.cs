using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models.User
{
    public class RequestResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
