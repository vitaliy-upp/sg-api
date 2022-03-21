using System;
using System.Threading.Tasks;

namespace FileManagement.Utilities
{
    public interface IFileManager
    {
        Task<string> UploadFileAsync();
    }
}
