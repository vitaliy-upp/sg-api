using Common.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserManagement.DataAccess.UserManagement.Profile
{
    public class Education : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }

    }
}
