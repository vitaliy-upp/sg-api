using System;
using System.Collections.Generic;
using System.Text;

namespace NoLimitTech.Domain.Models.UserManagement
{
    public class EmailVerificationToken: Token, IBaseDomainModel<int>
    {
        public int UserId { get; set; }
        public EmailVerificationToken() : base()
        {

        }
    }
}
