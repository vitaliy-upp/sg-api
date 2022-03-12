using System.Threading.Tasks;
using MiroWhiteBoard.Enums;
using MiroWhiteBoard.Interfaces;
using MiroWhiteBoard.Models;
using MiroWhiteBoard.Utils;
using Newtonsoft.Json;

namespace MiroWhiteBoard.Services
{
    public class MiroWhiteBoardService : IMiroWhiteBoardService
    {
        public string OAuth2Token { get; private set; }

        private SendRequestService _sendRequestService;

        public MiroWhiteBoardService()
        {
            _sendRequestService = new SendRequestService();
        }

        public void SetOAuthToken(string oauth2Token)
        {
            OAuth2Token = oauth2Token;
        }

        public async Task<BoardResponse> CreateBoardAsync(string name, SharingPolicyAccessEnum sharingPolicyAccess, SharingPolicyTeamAccessEnum sharingPolicyTeamAccess)
        {
            var data = new CreateBoardRequest()
            {
                Name = name,
                SharingPolicy = new SharingPolicyRequest()
                {
                    Access = sharingPolicyAccess.ToString().ToLower(),
                    TeamAccess = sharingPolicyTeamAccess.ToString().ToLower()
                }
            };
            string jsonRequest = JsonConvert.SerializeObject(data);

            string json = await _sendRequestService.SendPostRequest(MiroConstants.BoardsUrl, OAuth2Token, jsonRequest);

            BoardResponse boardCreated = JsonConvert.DeserializeObject<BoardResponse>(json);
            return boardCreated;
        }

        public async Task ClearBoardAsync(string boardId)
        {
            var responseString = await _sendRequestService.SendGetRequest(string.Format(MiroConstants.GetWidgetsUrl, boardId), OAuth2Token);
            var widgets = JsonConvert.DeserializeObject<GetWidgetsResponse>(responseString);
            foreach (var wdg in widgets.Data)
            {
                await _sendRequestService.SendDeleteRequest(string.Format(MiroConstants.DeleteWidgetUrl, boardId, wdg.Id), OAuth2Token);
            }
        }
    }
}
