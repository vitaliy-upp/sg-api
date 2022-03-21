using AutoMapper;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.BusinessLogic.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileManagement.Utilities.AzureBlob;

namespace Domain.BusinessLogic.Services
{
    public class MediaApplicationService : IMediaApplicationService
    {
        private readonly ILogger<MediaApplicationService> _logger;
        private readonly IMapper _mapper;

        private readonly SoundSettings _soundSettings;
        private readonly RecordingSettings _recordingSettings;
        private readonly BlobStorageSettings _storageConfig;

        public MediaApplicationService(ILogger<MediaApplicationService> logger
            , IMapper mapper
            , IConfiguration configuration
            )
        {
            _logger = logger;
            _mapper = mapper;

            _soundSettings = configuration.GetSection(nameof(SoundSettings)).Get<SoundSettings>();
            _recordingSettings = configuration.GetSection(nameof(RecordingSettings)).Get<RecordingSettings>();
            _storageConfig = configuration.GetSection(nameof(BlobStorageSettings)).Get<BlobStorageSettings>();
        }

        public async Task<string> UploadMediaAsync(IFormFile file)
        {
            string fileName = GenerateName(file.FileName);
            await UploadFileToStorageAsync(file, fileName, _storageConfig.ImageContainer);
            return fileName;
        }


        #region PRIVATE METHODS

        private string GenerateName(string oldFileName)
        {
            var splitted = oldFileName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            return string.Format("{0}.{1}", DateTime.Now.Ticks, splitted[splitted.Length - 1]);
        }

        private async Task UploadFileToStorageAsync(IFormFile file, string fileName, string container)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(file.OpenReadStream());
        }

        private async Task<bool> DeleteFileFromStorage(string fileName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConfig.ConnectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_storageConfig.SoundContainer);
            return await blobContainerClient.GetBlobClient(fileName).DeleteIfExistsAsync();
        }

        #endregion
    }
}
