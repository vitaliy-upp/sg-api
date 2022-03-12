using Common.DataAccess;

namespace NoLimitTech.Domain.Models
{
    public class UserCustomer : IBaseDomainModel<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerId { get; set; }
    }
}
