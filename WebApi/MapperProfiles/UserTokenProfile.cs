using AutoMapper;
using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Models;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class UserTokenProfile : Profile
    {
        public UserTokenProfile()
        {
            CreateMap<UserToken, UserTokenDto>()
                .ReverseMap();
        }
    }
}
