using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IKidProfileService : IBaseBusinessService
    {
        Task<IList<KidProfileDto>> FindByParentIdAsync(int parentId);

        /// <summary>
        /// Find kid profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<KidProfileDto> FindByIdAsync(int id);

        /// <summary>
        /// Create new Kid Profile
        /// </summary>
        /// <param name="kid profile"></param>
        /// <returns>KidProfileDto</returns>
        Task<KidProfileDto> CreateAsync(KidProfileDto user);


        /// <summary>
        /// Update kid profile
        /// </summary>
        /// <param name="kid profile id"></param>
        /// <param name="KidProfileDto"></param>
        Task<KidProfileDto> UpdateAsync(int id, KidProfileDto dto);
    }
}
