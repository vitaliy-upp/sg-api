using Common.DataAccess.Utilities;

namespace UserManagement.DataAccess.UserManagement.Location
{
    public class Country : IBaseDomainModel<short>
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Language { get; set; }

    }

}
