﻿using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.BusinessLogic.Models
{
    public class RegisterBaseModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Max length of a first name is 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max length of a last name is 50 characters")]
        public string LastName { get; set; }

        [Required]
        [PasswordValidation]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        [AllowedExtensions(Extensions = ".jpg,.jpeg,.png", ErrorMessage = "Wrong file format")]
        public IFormFile ImageFile { get; set; }

    }

    public class CompanyRegistration
    {


        [Required]
        [StringLength(100, ErrorMessage = "Max length of a company name is 100 characters")]
        public string CompanyName { get; set; }

        //[Required]
        [StringLength(150, ErrorMessage = "Max length of a company url is 150 characters")]
        public string CompanyUrl { get; set; }

        [StringLength(50, ErrorMessage = "Max length of a role in company is 50 characters")]
        public string RoleInCompany { get; set; }

        [StringLength(150, ErrorMessage = "Max length of a description is 150 characters")]
        public string Description { get; set; }
    }
    public class RegistrationModel : RegisterBaseModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }

    public class URegistrationModel : RegisterBaseModel
    {
        [Required]
        public string InviteKey { get; set; }
    }

}
