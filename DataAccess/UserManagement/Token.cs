using Common.DataAccess;
using System;
using System.ComponentModel.DataAnnotations;
using Utilities;

namespace DataAccess.UserManagement
{
    public class Token: ICreatedDate
    {
        [Key]
        public int Id { get; set; }
        public string TokenKey { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ExpiredDate { get; set; }
        public Token()
        {
            TokenKey = GuidUtils.GetTokenKey();
            CreatedDate = DateTime.UtcNow;
        }

        public void SetToken(string token)
        {
            TokenKey = token;
        }
    }
}
