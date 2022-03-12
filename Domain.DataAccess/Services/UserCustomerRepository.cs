using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using System.Linq;

namespace Domain.DataAccess.Services
{
    public class UserCustomerRepository : DomainService<UserCustomer, int>, IUserCustomerRepository
    {
        public UserCustomerRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }

        public UserCustomer GetByUserId(int id)
        {
            return Context.Set<UserCustomer>()
                .FirstOrDefault(x => x.UserId == id);
        }

        public UserCustomer GetByCustomerId(string id)
        {
            return Context.Set<UserCustomer>()
                .FirstOrDefault(x => x.CustomerId == id);
        }

        public UserCustomer Create(int userId, string customerId)
        {
            return Create(new UserCustomer()
            {
                UserId = userId,
                CustomerId = customerId
            });
        }
    }
}
