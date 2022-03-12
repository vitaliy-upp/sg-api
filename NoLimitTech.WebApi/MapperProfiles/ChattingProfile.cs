using AutoMapper;
using NoLimitTech.Application.Models;
using NoLimitTech.Domain.Models;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class ChattingProfile : Profile
    {
        public ChattingProfile()
        {

            // Chat messages
            CreateMap<Message, ChatMessageModel>();
        }
    }
}
