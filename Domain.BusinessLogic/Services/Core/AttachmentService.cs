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
using Domain.Repository.Interfaces.KidProfile;
using FileManagement.Utilities;
using FileManagement.DataAccess;

namespace Domain.BusinessLogic.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _repository;
        private readonly ILogger<AttachmentService> _logger;
        private readonly IFileManager _fileManager;

        private readonly IMapper _mapper;

        public AttachmentService(ILogger<AttachmentService> logger
            , IMapper mapper
            , IConfiguration configuration
            , IFileManager fileManager
            )
        {
            _logger = logger;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        public async Task<AttachmentDto> UploadAsync(AttachmentDto dto, bool shouldBeSaved = true)
        {
            var url = await _fileManager.UploadFileAsync(dto.File);
            dto.Url = url;
            var model = _mapper.Map<Attachment>(dto);
            var result = await _repository.CreateAsync(model, shouldBeSaved);
            return _mapper.Map<AttachmentDto>(result);
        }
    }
}
