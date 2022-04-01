using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FileManagement.Utilities
{
    public class FileManager: IFileManager
    {
        private readonly IFileProvider _fileManager;
        public FileManager(IFileProvider fileManager) {
            _fileManager = fileManager;
        }
        public async Task<string> UploadFileAsync(IFormFile file) {
            var result = await _fileManager.UploadFileAsync(file);
            return result;
        }
    }
}
