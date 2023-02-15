using DAL.DTO;
using DAL.Models;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ILogin
    {
        Task<int> GetIdByUsername(string username);


        Task<Logins> LoginEntAsync(LoginDto user);

    }
}
