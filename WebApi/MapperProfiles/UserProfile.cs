﻿using AutoMapper;
using DataAccess.UserManagement;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Models;
using FileManagement.DataAccess;
using System.Linq;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // USER

            CreateMap<User, UserDto>()
                .ForMember(d => d.Role, o => o.MapFrom(s => s.UserRoles.FirstOrDefault().RoleId))
                .ForMember(d => d.CompanyUrl, o => o.MapFrom(s => s.Company.Website));

            CreateMap<Attachment, SocialLinkDTO>();
            CreateMap<SocialLinkDTO, Attachment>();

            CreateMap<UserDto, User>();

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
