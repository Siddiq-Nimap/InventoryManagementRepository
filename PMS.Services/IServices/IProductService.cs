using PMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.IServices
{
    public interface IProductService
    { 
            Task<IEnumerable<Product>> GetModel();

            Product GetModelById(int id);

            bool InsertModel(Product Model);

            bool UpdateModel(Product Model);

            bool DeleteModel(int id);

            Task<bool> Save();

            Task<IEnumerable<Product>> Paging(int PagingNbr);

            int TotalPages();
    }
}
