using AutoMapper;
using Domain.BusinessLogic.Models;
using Domain.DataAccess.Models;

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
