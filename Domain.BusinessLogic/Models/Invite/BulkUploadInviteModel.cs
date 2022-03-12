using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Attributes;

namespace Domain.BusinessLogic.Models
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
