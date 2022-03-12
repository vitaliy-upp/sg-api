using Microsoft.AspNetCore.Http;
using NoLimitTech.Application.Models;
using NoLimitTech.Application.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoLimitTech.Application.ServiceInterfaces
{
    public interface IMediaApplicationService : IApplicationService
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
