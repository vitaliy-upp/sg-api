using Common.DataAccess.Utilities;

namespace UserManagement.DataAccess.UserManagement.Location
{
    public class Region : IBaseDomainModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short CountryId { get; set; }

        public Country Country { get; set; }
    }

}
