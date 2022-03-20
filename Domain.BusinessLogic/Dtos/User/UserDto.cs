using System.Collections.Generic;
using Newtonsoft.Json;
using Domain.BusinessLogic.Enums;
using System;

namespace Domain.BusinessLogic.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRolesEnum Role { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string RoleInCompany { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ImageUrl => Image;

        public CompanyModel Company { get; set; }
        public IList<SocialLinkDTO> SocialLinks { get; set; }

        [JsonIgnore]
        public bool IsEmailVerified { get; set; }
    }
}
