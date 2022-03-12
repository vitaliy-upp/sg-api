using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
