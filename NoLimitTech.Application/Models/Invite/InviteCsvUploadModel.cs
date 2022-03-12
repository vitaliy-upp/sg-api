using System;
using NoLimitTech.Application.Enums;
using NoLimitTech.Domain.Enums;

namespace NoLimitTech.Application.Models
{
    public class InviteCsvUploadModel
    {
        public string Email { get; set; }

        public UserRolesEnum Role { get; set; }
    }
}
