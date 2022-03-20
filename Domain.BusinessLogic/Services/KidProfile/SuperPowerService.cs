using AutoMapper;
using Domain.BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Domain.Repository.Interfaces.KidProfile;
using Domain.BusinessLogic.Models;
using Domain.Repository;
using Domain.DataAccess.Entities.KidProfile;

namespace Domain.BusinessLogic.Services
{
    public class SuperPowerService : BusinessService<SuperPower, int, SuperPowerDto>, ISuperPowerService
    {
        public SuperPowerService(IHttpContextAccessor httpContextAccessor
            , ISuperPowerRepository repository
            , IMapper mapper
            ) : base(httpContextAccessor
                , repository
                , mapper)
        {
        }

    }
}
