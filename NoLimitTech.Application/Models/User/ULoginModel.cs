using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class ULoginModel
    {
        [Required]
        public string Token { get; set; }

        public string Username { get; set; }

        public IFormFile Image { get; set; }
    }
}
