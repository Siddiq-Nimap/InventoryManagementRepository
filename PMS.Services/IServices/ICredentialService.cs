using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.IServices
{
    public interface ICredentialService 
    {
        string GetToken(Logins user);

        ClaimsPrincipal ValidationToken(string token);

        
        Task<int> GetIdByUsername(string username);

        Task<Logins> LoginEntAsync(LoginDto user);

        bool InsertModel(Logins logins);

        Task<bool> Save();
    }
}
