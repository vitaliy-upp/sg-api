using Microsoft.AspNetCore.Http;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.ServiceInterfaces
{
    public interface IMediaApplicationService : IBaseBusinessService
    {
        /// <summary>
        /// Upload image to the azure blob storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="storageConfig"></param>
        /// <returns>New name of the file</returns>
        Task<string> UploadMediaAsync(IFormFile file);
    }
}
