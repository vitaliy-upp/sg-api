using Common.DataAccess.Utilities;
using DataAccess.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UserManagement.DataAccess.UserManagement.Location;

namespace Domain.DataAccess.Entities.KidProfile.Education
{
    public class Education : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public int KidProfileId { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public bool IsUpToPresent { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public KidProfile KidProfile { get; set; }
        public string Grade { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
