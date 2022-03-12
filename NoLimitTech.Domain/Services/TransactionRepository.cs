using NoLimitTech.Domain.ServiceInterfaces;
using Payment.DataAccess.Enitities;

namespace NoLimitTech.Domain.Services
{
    public class TransactionRepository : DomainService<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(DomainDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
