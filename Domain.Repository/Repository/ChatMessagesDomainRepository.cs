using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;
using Domain.Repository;
using System.Threading.Tasks;

namespace Domain.DataAccess.Services
{
    public class ChatMessagesDomainRepository : DomainRepository<Message, int>, IChatMessagesDomainService
    {
        public ChatMessagesDomainRepository(DomainDbContext dbContext)
            :base(dbContext)
        {}
    }
}
