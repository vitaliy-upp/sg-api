using System.Collections.Generic;
using DataAccess.UserManagement;
using Domain.Repository;

namespace Domain.DataAccess.ServiceInterfaces
{
    public interface ISocialLinkRepository : IDomainRepository<ExternalLink, int>
    {
        /// <summary>
        /// Create social links records
        /// </summary>
        /// <param name="socialLinks"></param>
        void CreateAll(IEnumerable<ExternalLink> socialLinks);

        /// <summary>
        /// Get social links for user
        /// </summary>
        /// <param name="socialLinks"></param>
        IList<ExternalLink> GetForUser(int userId);
    }
}
