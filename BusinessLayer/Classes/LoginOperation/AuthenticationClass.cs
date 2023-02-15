using CrudOperations.Interfaces;
using DAL.Models;
using Microsoft.IdentityModel.Tokens;
//using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace CrudOperations.Business_Layer
{
    public class AuthenticationClass : IAuthenticationManager
    {
         private static string Key = Guid.NewGuid().ToString();
        private static string Issuer = "https://localhost:44370/";
        private static string Audience = "https://localhost:44370/";
        public string GenerateToken(Logins user)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, user.UserName),
               //new Claim(ClaimTypes.Email, user.Email_Id),
               //new Claim(ClaimTypes.GivenName, user.FirstName),
               //new Claim(ClaimTypes.Surname, user.LastName),
               new Claim(ClaimTypes.Role, user.Roles)
            };


            var token = new JwtSecurityToken(Issuer, Audience, 
                claims, expires: DateTime.Now.AddDays(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidationToken(string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            var ValidationParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:44370/",
                ValidAudience = "https://localhost:44370/",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
            };
            tokenhandler.ValidateToken(token, ValidationParams, out SecurityToken ValidatedToken);

            JwtSecurityToken JwtToken = (JwtSecurityToken)ValidatedToken;
            var identity = new ClaimsIdentity(JwtToken.Claims, "Bearer");
            
            return new ClaimsPrincipal(identity);
        }
    }
}