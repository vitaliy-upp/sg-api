using Domain.BusinessLogic.Enums;
using Domain.DataAccess.Enums;

namespace Domain.BusinessLogic.Models
{
    public class InviteModel
    {
        public int Id { get; set; }
        public string TokenKey { get; set; }
        public string UserEmail { get; set; }
        public UserRolesEnum EventRole { get; set; }
    }
}
