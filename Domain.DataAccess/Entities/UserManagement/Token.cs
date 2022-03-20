using System;

namespace NoLimitTech.Domain.Models.UserManagement
{
    public class Token
    {
        public int Id { get; set; }
        public string TokenKey { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ExpiredDate { get; set; }

        public Token() {
            TokenKey = Guid.NewGuid().ToString();
            CreatedDate = DateTime.UtcNow;
        }
    }
}
