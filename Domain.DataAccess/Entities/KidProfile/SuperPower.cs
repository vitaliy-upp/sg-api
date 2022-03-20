using Common.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class SuperPower : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? ParentId { get; set; }
        public SuperPower Parent { get; set; }
    }
}
