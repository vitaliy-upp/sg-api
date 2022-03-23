using AutoMapper;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Models;
using FileManagement.DataAccess;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class FilesProfile : Profile
    {
        public FilesProfile()
        {
            CreateMap<AttachmentDto, Attachment>().ReverseMap();
        }
    }
}
