using System;
using Domain.BusinessLogic.Enums;
using Domain.DataAccess.Enums;

namespace Domain.BusinessLogic.Models
{
    public class InviteCsvUploadModel
    {
        public string Email { get; set; }

        public UserRolesEnum Role { get; set; }
    }
}
