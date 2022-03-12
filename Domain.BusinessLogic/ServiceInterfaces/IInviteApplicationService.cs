using System.Collections.Generic;
using Domain.BusinessLogic.Models;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IInviteApplicationService : IApplicationService
    {

        /// <summary>
        /// Create invite to event for each user
        /// </summary>
        /// <param name="model"></param>
        void Create(CreateInviteModel model);

        /// <summary>
        /// Get invite to cenference by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        InviteModel FindByToken(string token);

        /// <summary>
        /// Find Invite by user email and event id
        /// </summary>
        /// <param name="email"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        InviteModel FindByUserEmailAndEventId(string email, int eventId);

        /// <summary>
        /// Get Invite by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        InviteModel FindByUserEmail(string email);
        
        /// <summary>
        /// Get Invites for event with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<InviteModel> FindByEventId(int id);
        
        /// <summary>
        /// Delete invitation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Create invites to event for each user from csv file
        /// </summary>
        /// <param name="model"></param>
        void BulkUpload(BulkUploadInviteModel model);
    }
}
