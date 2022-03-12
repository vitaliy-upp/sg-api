using Common.DataAccess;
using DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.UserManagement
{
    public class Role : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
