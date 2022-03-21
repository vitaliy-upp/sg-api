using System.Collections.Generic;
using Domain.Repository;
using FileManagement.DataAccess;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface ISocialLinkRepository : IDomainRepository<Attachment, int>
    {
        /// <summary>
        /// Create social links records
        /// </summary>
        /// <param name="socialLinks"></param>
        void CreateAll(IEnumerable<Attachment> socialLinks);

        /// <summary>
        /// Get social links for user
        /// </summary>
        /// <param name="socialLinks"></param>
        IList<Attachment> GetForUser(int userId);
    }
}
