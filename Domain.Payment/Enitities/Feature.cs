using Common.DataAccess;

namespace Payment.DataAccess.Enitities
{
    public class Feature : IBaseDomainModel<int> 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// This key is unique and will be in use on the front-end to understand what is allowed for the user.
        /// The field is not editable from an admin or any other user end as it requeires front-end changes.
        /// </summary>
        public string Key { get; set; }
    }
}
