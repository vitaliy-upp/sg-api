using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class ULoginModel
    {
        [Required]
        public string Token { get; set; }

        public string Username { get; set; }

        public IFormFile Image { get; set; }
    }
}
