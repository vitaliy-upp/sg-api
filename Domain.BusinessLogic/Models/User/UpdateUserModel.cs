using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class UpdateUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Max length of a company name is 50 characters")]
        public string CompanyName { get; set; }

        [StringLength(150, ErrorMessage = "Max length of a company url is 150 characters")]
        public string CompanyUrl { get; set; }

        public string RoleInCompany { get; set; }

        [StringLength(150, ErrorMessage = "Max length of description is 150 characters.")]
        public string Description { get; set; }

        //[AllowedExtensions(Extensions = ".jpg,.jpeg,.png", ErrorMessage = "Wrong file format.")]
        public IFormFile Image {
            get { return ImageFile; }
            set { ImageFile = value; }
        }
        public IFormFile ImageFile { get; set; }

        public IList<SocialLinkDTO> SocialLinks { get; set; }

    }
}
