using BusinessLayer.Repositories;
using PMS.Data;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using PMS.Services.IServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PMS.Services.Services
{
    public class CredentialService :AllRepository<Logins> , ICredentialService
    {
        readonly CredentialManager _Credentials;
        public CredentialService(Context productdb, CredentialManager credential) : base(productdb)
        {
            _Credentials = credential;
        }
        public string GetToken(Logins user)
        {
            var data = _Credentials.GenerateToken(user);
            return data;
        }

        public Task<int> GetIdByUsername(string username)
        {
            var data = _Credentials.GetIdByUsername(username);
            return data;
        }

        public Task<Logins> LoginEntAsync(LoginDto user)
        {
            var data = _Credentials.LoginEntAsync(user);
            return data;
        }

        public ClaimsPrincipal ValidationToken(string token)
        {
            var data = _Credentials.ValidationToken(token);
            return data;
        }

        public bool InsertModel(Logins logins)
        {
            var data = insertModel(logins);
            return data;
        }

        public async Task<bool> Save()
        {
            var data = await save();
            return data;
        }
    }
}
