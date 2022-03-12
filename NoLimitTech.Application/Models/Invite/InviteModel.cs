using NoLimitTech.Application.Enums;
using NoLimitTech.Domain.Enums;

namespace NoLimitTech.Application.Models
{
    public class InviteModel
    {
        public int Id { get; set; }
        public string TokenKey { get; set; }
        public string UserEmail { get; set; }
        public UserRolesEnum EventRole { get; set; }
    }
}
