using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICategoryInsert
    {
        Task<bool> InsertProductInCatagoryAsync(int productId, int CatagoryId);

    }
}
