using MiroWhiteBoard.Enums;
using MiroWhiteBoard.Models;
using System.Threading.Tasks;

namespace MiroWhiteBoard.Interfaces
{
    public interface IMiroWhiteBoardService
    {
        Task<BoardResponse> CreateBoardAsync(string name, SharingPolicyAccessEnum sharingPolicyAccess, SharingPolicyTeamAccessEnum sharingPolicyTeamAccess);

        void SetOAuthToken(string oauth2Token);
        Task ClearBoardAsync(string boardId);
    }
}
