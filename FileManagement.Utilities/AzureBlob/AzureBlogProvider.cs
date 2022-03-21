using FileManagement.Utilities.AzureBlob;
using System;
using System.Threading.Tasks;
//using Azure.Storage;
//using Azure.Storage.Blobs;

namespace FileManagement.Utilities
{
    public class AzureBlogProvider : IFileProvider
    {
        private readonly BlobStorageSettings _storageConfig;
        public AzureBlogProvider(
            //IConfiguration configuration 
            ) {

            //_storageConfig = configuration.GetSection(nameof(BlobStorageSettings)).Get<BlobStorageSettings>();
        }

        public async Task<string> UploadFileAsync(/*IFormFile file*/object file)
        {
            //string fileName = GenerateName(file.FileName);
            //await UploadFileToStorageAsync(file, fileName, _storageConfig.ImageContainer);
            throw new NotImplementedException();
        }

        private string GenerateName(string oldFileName)
        {
            var splitted = oldFileName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            return string.Format("{0}.{1}", DateTime.Now.Ticks, splitted[splitted.Length - 1]);
        }

        private async Task UploadFileToStorageAsync(/*IFormFile file,*/ string fileName, string container)
        {
        //    BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
        //    BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(container);
        //    BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        //    await blobClient.UploadAsync(file.OpenReadStream());
        }

        private async Task<bool> DeleteFileFromStorage(string fileName)
        {
            //BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            //BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.SoundContainer);
            //return await blobContainerClient.GetBlobClient(fileName).DeleteIfExistsAsync();
            throw new NotImplementedException();
        }
    }
}
