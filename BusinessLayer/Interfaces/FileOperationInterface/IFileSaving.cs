
using DAL.Models;


namespace CrudOperations.Interfaces
{
    public interface IFileSaving
    {
        string FileUpload(Product pro);

        bool FileDelete(string Image);

   
    }
}
