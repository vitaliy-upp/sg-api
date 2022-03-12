using Common.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.UserManagement
{
    public class User : IBaseDomainModel<int>, ICreatedDate
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime CreatedDate { get; private set; }
        public ICollection<UserRoles> UserRoles { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public User()
        {
            CreatedDate = Utilities.DateTimeUtils.Now();
        }
    }
}
