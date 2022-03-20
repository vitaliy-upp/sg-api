using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using Payment.DataAccess.Enitities;

namespace Domain.DataAccess.Services
{
    public class TransactionRepository : DomainRepository<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(DomainDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
