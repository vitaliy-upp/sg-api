using NoLimitTech.Domain.Models.UserManagement;
using System;

namespace NoLimitTech.Domain.Models
{
    public class UserToken : Token, IBaseDomainModel<int>
    {
        public int UserId { get; set; }
        public UserToken(): base() { 
        
        }
    }
}
