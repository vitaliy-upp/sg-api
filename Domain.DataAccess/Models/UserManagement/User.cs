using System.Collections.Generic;

namespace NoLimitTech.Domain.Models
{
    public class User : IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string RoleInCompany { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
