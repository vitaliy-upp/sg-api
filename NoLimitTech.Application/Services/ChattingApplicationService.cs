using AutoMapper;
using NoLimitTech.Application.Models;
using NoLimitTech.Application.ServiceInterfaces;
using NoLimitTech.Domain.Enums;
using NoLimitTech.Domain.Models;
using NoLimitTech.Domain.ServiceInterfaces;

namespace NoLimitTech.Application.Services
{
    public class ChattingApplicationService : IChattingApplicationService
    {
        private readonly IChatMessagesDomainService _chatMessagesDomainService;
        private readonly IMapper _mapper;

        public ChattingApplicationService(IMapper mapper
            , IChatMessagesDomainService chatMessagesDomainService)
        {
            _chatMessagesDomainService = chatMessagesDomainService;
            _mapper = mapper;
        }

        public ChatMessageModel SaveMessage(int fromUserId, int eventId, ESSendMessageModel model)
        {
            Message message = new Message()
            {
                FromUserId = fromUserId,
                Text = model.Text,
                ToConferenceId = eventId,
                ToUserId = model.ToUserId,
                ToDeskId = model.ToDeskId,
                ToFloorId = model.ToFloorId,
                Type = MessageTypeEnum.Regular
            };
            message = _chatMessagesDomainService.Create(message);
            return _mapper.Map<ChatMessageModel>(message);
        }
    }
}
