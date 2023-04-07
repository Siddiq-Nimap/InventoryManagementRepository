using PMS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Services.IServices
{
    public interface ICatagoryService 
    {
        Task<IEnumerable<Catagory>> GetModel();

        Catagory GetModelById(int id);

        bool InsertModel(Catagory Model);

        bool UpdateModel(Catagory Model);

        bool DeleteModel(int id);

        Task<bool> Save();

        Task<IEnumerable<Catagory>>Paging(int PagingNbr);

        int TotalPages();
    }
}
