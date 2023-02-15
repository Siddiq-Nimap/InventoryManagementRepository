using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface IPaging
    {
        Task<T> Paging<T>(int PaginNbr,string returntype);

        int TotalPages();
    }
}
