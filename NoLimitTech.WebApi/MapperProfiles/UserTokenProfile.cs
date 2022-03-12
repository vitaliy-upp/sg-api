using AutoMapper;
using DataAccess.UserManagement;
using NoLimitTech.Application.Models;
using NoLimitTech.Domain.Models;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class UserTokenProfile : Profile
    {
        public UserTokenProfile()
        {
            CreateMap<UserToken, UserTokenModel>()
                .ReverseMap();
        }
    }
}
