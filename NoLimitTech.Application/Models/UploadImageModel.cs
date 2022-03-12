using Microsoft.AspNetCore.Http;
using NoLimitTech.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace NoLimitTech.Application.Models
{
    public class UploadImageModel
    {
        [Required]
        [AllowedExtensions(Extensions = ".jpg,.jpeg,.png", ErrorMessage = "Wrong file format")]
        public IFormFile Image { get; set; }
        //public CancellationToken cancellationToken { get; set; }
    }
}
