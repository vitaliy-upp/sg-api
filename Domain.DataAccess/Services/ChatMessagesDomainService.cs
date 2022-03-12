using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;

namespace Domain.DataAccess.Services
{
    public class ChatMessagesDomainService : DomainService<Message, int>, IChatMessagesDomainService
    {
        public ChatMessagesDomainService(DomainDbContext dbContext)
            :base(dbContext)
        {}
    }
}
