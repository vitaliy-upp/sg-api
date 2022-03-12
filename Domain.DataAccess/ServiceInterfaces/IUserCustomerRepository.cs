using Domain.DataAccess.Models;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IUserCustomerRepository : IDomainService<UserCustomer, int>
    {
        /// <summary>
        /// Get User Customer by User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserCustomer GetByUserId(int id);

        /// <summary>
        /// Get UserCustomer by customer id of stripe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserCustomer GetByCustomerId(string id);

        /// <summary>
        /// Create User Customer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        UserCustomer Create(int userId, string customerId);
    }
}
