using System;
using System.Collections.Generic;
using System.Text;

namespace NoLimitTech.Domain.Models
{
    public class Company : IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }

    }
}
