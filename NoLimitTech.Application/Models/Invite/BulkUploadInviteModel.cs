using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NoLimitTech.Application.Attributes;

namespace NoLimitTech.Application.Models
{
    public class BulkUploadInviteModel
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        [AllowedExtensions(Extensions = ".csv", ErrorMessage = "Wrong file format")]
        public IFormFile File { get; set; }
    }
}
