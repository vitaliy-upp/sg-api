using Microsoft.AspNetCore.Http;
using Common.DataAccess.Utilities;
using Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;

namespace Domain.BusinessLogic.Services
{
    public class BusinessService<TModel, TId, TDto>: BaseBusinessService where TModel : class, IBaseDomainModel<TId>
    {
        private readonly IDomainRepository<TModel, TId> _repository;
        private readonly IMapper _mapper;
        public BusinessService(IHttpContextAccessor httpContextAccessor
            , IDomainRepository<TModel, TId> repository
            , IMapper mapper)
            :base(httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<TModel>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(t => _mapper.Map<TModel>(t)).ToList();
        }
    }
}
