using System.Threading.Tasks;

namespace FileManagement.Utilities
{
    public class FileManager: IFileManager
    {
        private readonly IFileManager _fileManager;
        public FileManager(IFileManager fileManager) {
            _fileManager = fileManager;
        }
        public async Task<string> UploadFileAsync() {
            var result = await _fileManager.UploadFileAsync();
            return result;
        }
    }
}
