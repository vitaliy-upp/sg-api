using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models.User
{
    public class EmailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
