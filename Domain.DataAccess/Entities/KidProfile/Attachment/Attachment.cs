using System;
using System.ComponentModel.DataAnnotations;
using Common.DataAccess.Utilities;
using Domain.DataAccess.Entities.KidProfile;

namespace DataAccess.UserManagement
{
    public class Attachment : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
