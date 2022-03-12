using AutoMapper;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.ServiceInterfaces;
using Domain.DataAccess.Enums;
using Domain.DataAccess.Models;
using Domain.DataAccess.ServiceInterfaces;

namespace Domain.BusinessLogic.Services
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
