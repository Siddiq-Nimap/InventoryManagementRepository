
using PMS.Models.Models;
using PMS.Models.Models.DTO;

namespace PMS.Data.IRepository
{
    public interface IFileUpload
    {
        string FileUpload(Product pro);

        bool FileDelete(string Image);

   
    }
}
