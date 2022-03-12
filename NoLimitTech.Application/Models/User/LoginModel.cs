using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
