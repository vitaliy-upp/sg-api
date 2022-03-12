using Payment.DataAccess.Enitities;

namespace NoLimitTech.Domain.ServiceInterfaces
{
    public interface ITransactionRepository : IDomainService<Transaction, int>
    {
    }
}
