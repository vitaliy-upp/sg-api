using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Domain.BusinessLogic.Enums;
using Domain.BusinessLogic.Models;
using Domain.BusinessLogic.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BusinessLogic.Utils
{
    public static class JwtTokenUtils
    {
        /// <summary>
        /// Generate Jwt Token
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static string GenerateJwtToken(string userEmail, UserRolesEnum userRole, bool isRegistered, bool byInvite, JwtSettings jwtSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = CreateTokenClaims(userEmail, userRole.ToString(), isRegistered, byInvite, jwtSettings);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(jwtSettings.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Create Token Claims
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static List<Claim> CreateTokenClaims(string userEmail, string userRole, bool isRegistered, bool byInvite, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtSettings.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim(ClaimTypes.Role, userRole),

                new Claim(AppConstants.IS_REGISTERED, isRegistered.ToString()),
                new Claim(AppConstants.BY_INVITE, byInvite.ToString())
            };
            return claims;
        }

        /// <summary>
        /// Create Token validation parameters
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <returns>TokenValidationParameters</returns>
        public static TokenValidationParameters GetTokenValidationParams(JwtSettings jwtSettings)
        {
            return new TokenValidationParameters
            {
                // whether validate an issuer
                ValidateIssuer = true,
                // set up issuer
                ValidIssuer = jwtSettings.Issuer,

                // whether validate an audience
                ValidateAudience = true,
                // set up audience
                ValidAudience = jwtSettings.Audience,
                // whether validate a key life time
                ValidateLifetime = true,

                // set up security key
                IssuerSigningKey = AuthSettings.GetSymmetricSecurityKey(jwtSettings.SecretKey),
                // validate security key
                ValidateIssuerSigningKey = true,

            };
        }

        /// <summary>
        /// Get Jwt Bearer Events
        /// </summary>
        /// <returns>JwtBearerEvents</returns>
        public static JwtBearerEvents GetJwtBearerEvents()
        {
            return new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/custom"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }
}
