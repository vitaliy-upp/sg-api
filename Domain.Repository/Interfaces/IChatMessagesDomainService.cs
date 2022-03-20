using Domain.DataAccess.Models;
using Domain.Repository;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface IChatMessagesDomainService : IDomainRepository<Message, int>
    {

    }
}
