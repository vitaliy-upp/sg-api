using AutoMapper;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Models;
using UserManagement.DataAccess.UserManagement.Location;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class KidProfileMap : Profile
    {
        public KidProfileMap()
        {
            CreateMap<KidProfile, KidProfileDto>().ReverseMap();
            CreateMap<SuperPower, SuperPowerDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();

        }
    }
}
