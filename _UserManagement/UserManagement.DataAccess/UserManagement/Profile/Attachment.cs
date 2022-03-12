using System;
using System.ComponentModel.DataAnnotations;
using Common.DataAccess.Utilities;
using UserManagement.DataAccess.UserManagement.Profile;

namespace DataAccess.UserManagement
{
    public class Attachment : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int KidProfileId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }

        public KidProfile KidProfile { get; set; }
    }
}
