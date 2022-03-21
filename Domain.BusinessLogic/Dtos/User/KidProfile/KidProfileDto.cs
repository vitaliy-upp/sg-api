using System.Collections.Generic;
using Newtonsoft.Json;
using Domain.BusinessLogic.Enums;
using System;

namespace Domain.BusinessLogic.Models
{
    public class KidProfileDto
    {
        public int Id { get; set; }

        public int ParrentId { get; set; }
        public string Url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Impairment { get; set; }
        public int CityId { get; set; }


        public IList<SuperPowerDto> SuperPowers { get; set; }
    }
}