using Common.DataAccess.Utilities;
using DataAccess.UserManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.DataAccess.UserManagement.Location;

namespace Domain.DataAccess.Entities.KidProfile
{
    public class KidProfile: IBaseDomainModel<int>
    {
        [Key]
        public int Id { get; set; }

        public int ParrentId { get; set; }
        public string Url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Impairment { get; set; }

        public int CityId { get; set; }

        public User Parrent { get; set; }
        public City City { get; set; }

        public IList<SuperPowerToKid> SuperPowers { get; set; }

    }
}
