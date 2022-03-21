using AutoMapper;
using Domain.BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Domain.Repository.Interfaces.KidProfile;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Entities.KidProfile.Education;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Domain.BusinessLogic.Services
{
    public class KidEducationService : BaseBusinessService, IKidEducationService
    {
        private readonly IKidEducationRepository _repository;
        private readonly IKidProfileService _kidService;

        private readonly IMapper _mapper;
        public KidEducationService(IHttpContextAccessor httpContextAccessor
            , IKidEducationRepository repository
            , IKidProfileService kidService
            , IMapper mapper
            ) : base(httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _kidService = kidService;
        }
        
        public async Task<EducationProfileDto> GetByKidIdAsync(int kidId)
        {
            var list = await _repository.GetByKidId(kidId);
            var result = new EducationProfileDto();
            result.Items = list.Select(t => _mapper.Map<EducationItemDto>(t)).ToList();
            return result;
        }

        public async Task CreateAsync(int kidId, int parentId, EducationProfileDto model)
        {
            var kid = await _kidService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("A kid was not found.");
            }

            foreach (var item in model.Items)
            {
                var ed = _mapper.Map<Education>(item);
                ed.KidProfileId = kidId;
                await _repository.CreateAsync(ed, false);
            }

            await _repository.SaveChangesAsync();
        }


        public async Task UpdateAsync(int kidId, int parentId, EducationProfileDto model)
        {
            var kid = await _kidService.GetByIdAsync(kidId);
            if (kid.ParrentId != parentId)
            {
                throw new Exception("A kid was not found.");
            }

            foreach (var item in model.Items)
            {
                var ed = _mapper.Map<Education>(item);
                await _repository.UpdateAsync(ed, false);
            }

            await _repository.SaveChangesAsync();
        }

    }
}
