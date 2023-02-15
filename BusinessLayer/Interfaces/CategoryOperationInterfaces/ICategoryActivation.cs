using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICategoryActivation
    {
        Task<bool> DeActivateAsync(int id);

        Task<bool> ActivateAsync(int id);
    }
}
