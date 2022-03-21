using Common.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class PersonalityQuestion : IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
    }
}
