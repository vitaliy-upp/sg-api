using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;

namespace NoLimitTech.Domain.Services
{
    public class ChatMessagesDomainService : DomainService<Message, int>, IChatMessagesDomainService
    {
        public ChatMessagesDomainService(DomainDbContext dbContext)
            :base(dbContext)
        {}
    }
}
