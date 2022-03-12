using System;

namespace Domain.BusinessLogic.Models
{
    public class UserTokenModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
