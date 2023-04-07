using BusinessLayer.IRepositories;
using Microsoft.IdentityModel.Tokens;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class CredentialManager : ICredentialManager
    {
        readonly Context productdb;
        public CredentialManager()
        {

            productdb = new Context();

        }


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

        public async Task<int> GetIdByUsername(string username)
        {
            var data = await productdb.Logins.SingleAsync(model => model.UserName == username);

            return data.Id;
        }

        public async Task<Logins> LoginEntAsync(LoginDto user)
        {
            var credentials = await productdb.Logins.FirstOrDefaultAsync(model => model.UserName == user.UserName && model.Password == user.Password);
            if (credentials == null) { return null; }
            else { return credentials; }
        }
    }
}
