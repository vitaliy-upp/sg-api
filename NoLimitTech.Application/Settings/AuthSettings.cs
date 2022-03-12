using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NoLimitTech.Application.Settings
{
    public class AuthSettings
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
