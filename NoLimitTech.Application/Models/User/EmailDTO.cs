using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models.User
{
    public class EmailDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
