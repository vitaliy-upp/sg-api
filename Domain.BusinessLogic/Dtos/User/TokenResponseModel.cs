namespace Domain.BusinessLogic.Models
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public UserDto UserDetails { get; set; }
    }

    public class uTokenResponseModel
    {
        public string Token { get; set; }
        public UserDto UserDetails { get; set; }
    }
}
