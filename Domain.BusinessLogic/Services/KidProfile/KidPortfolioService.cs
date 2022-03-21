using AutoMapper;
using Microsoft.Extensions.Configuration;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.ServiceInterfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Domain.DataAccess.Services;
using System.Collections.Generic;

namespace Domain.BusinessLogic.Services
{
    public class KidPortfolioService : BaseBusinessService, IKidPortfolioService
    {
        private readonly IKidPortfolioRepository _repository;
        private readonly IKidProfileService _kidProfileService;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;


        public KidPortfolioService(IHttpContextAccessor httpContextAccessor
            , IMapper mapper
            , IKidPortfolioRepository repository
            , IKidProfileService kidProfileService
            , IAttachmentService attachmentService
            ) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _kidProfileService = kidProfileService;
            _attachmentService = attachmentService;
        }

        public async Task CreateAsync(int kidId, int parentId, AttachmentDto dto)
        {
            var kid = await _kidProfileService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("Kid was not found");
            }

            var url = await _attachmentService.UploadAsync(dto.File);
            
        }

        public async Task DeleteAsync(int kidId, int parentId, int id)
        {
            var kid = await _kidProfileService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("Kid was not found");
            }

            await _repository.DeleteAsync(kidId, id, true);
        }

        public async Task<IList<KidPortfolioDto>> GetAsync(int kidId)
        {
            var list = await _repository.GetByKidtId(kidId);
            var result = list.Select(t => _mapper.Map<KidPortfolioDto>(t)).ToList();
            return result;

        }
    }
}
