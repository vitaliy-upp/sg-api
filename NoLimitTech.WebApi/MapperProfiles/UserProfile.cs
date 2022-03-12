using AutoMapper;
using DataAccess.UserManagement;
using NoLimitTech.Application.Models;
using NoLimitTech.Domain.Models;
using System.Linq;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // USER

            CreateMap<User, UserModel>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.UserRoles.FirstOrDefault().RoleId))
                .ForMember(d => d.CompanyUrl, o => o.MapFrom(s => s.Company.Website));

            CreateMap<ExternalLink, SocialLinkDTO>();
            CreateMap<SocialLinkDTO, ExternalLink>();

            CreateMap<UserModel, User>();

            CreateMap<RegistrationModel, User>()
                .ForMember(d => d.Password, opt => opt.Ignore());

            CreateMap<URegistrationModel, User>()
                .ForMember(d => d.Password, opt => opt.Ignore());

            CreateMap<RegistrationModel, URegistrationModel>().ReverseMap();

            CreateMap<UpdateUserModel, User>();


         
            CreateMap<Company, CompanyModel>();
        }
    }
}
