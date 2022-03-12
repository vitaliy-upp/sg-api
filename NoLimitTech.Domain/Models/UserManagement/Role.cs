using System.Collections.Generic;

namespace NoLimitTech.Domain.Models
{
    public class Role : IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
