using Common.DataAccess.Utilities;
using DataAccess.UserManagement;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class Recommendation : IBaseDomainModel<int>, ICreatedDate
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public int KidId { get; set; }
        public KidProfile KidProfile { get; set; }
        public User User { get; set; }
    }
}
