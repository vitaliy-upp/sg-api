using Domain.Repository;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface ITransactionRepository : IDomainRepository<Transaction, int>
    {
    }
}
