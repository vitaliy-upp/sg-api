using Domain.DataAccess.ServiceInterfaces;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.Services
{
    public class TransactionRepository : DomainService<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(DomainDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
