using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface ITransactionRepository : IDomainService<Transaction, int>
    {
    }
}
