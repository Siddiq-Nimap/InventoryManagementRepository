
using DAL.Models;
using System.Security.Claims;

namespace CrudOperations.Interfaces
{
    public interface IAuthenticationManager
    {
        string GenerateToken(Logins user);

        ClaimsPrincipal ValidationToken(string token);

    }
}
