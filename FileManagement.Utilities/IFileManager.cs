using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FileManagement.Utilities
{
    public interface IFileManager
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
