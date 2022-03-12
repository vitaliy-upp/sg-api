using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class UploadImageModel
    {
        [Required]
        [AllowedExtensions(Extensions = ".jpg,.jpeg,.png", ErrorMessage = "Wrong file format")]
        public IFormFile Image { get; set; }
        //public CancellationToken cancellationToken { get; set; }
    }
}
