using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models.User
{
    public class RequestResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
