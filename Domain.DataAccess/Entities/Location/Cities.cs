using Common.DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.DataAccess.UserManagement.Location
{
    public class City : IBaseDomainModel<int>
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public string Name { get; set; }


        public Region Region { get; set; }
    }

}
