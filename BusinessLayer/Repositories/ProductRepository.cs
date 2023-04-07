using BusinessLayer.IRepositories;
using PMS.Data;
using PMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class ProductRepository : AllRepository<Product>, IProductRepository
    {
        public ProductRepository(Context productdb) : base(productdb)
        {
            
        }
        public bool DeleteModel(int id)
        {
            var data = deleteModel(id);
            return data;
        }

        public async Task<IEnumerable<Product>> GetModel()
        {
            var data = await getModel();
            return data;
        }

        public Product GetModelById(int id)
        {
            var data = getModelById(id);
            return data;
        }

        public bool InsertModel(Product Model)
        {
            var data = insertModel(Model);
            return data;
        }

        public async Task<bool> Save()
        {
            var data = await save();
            return data;
        }

        public bool UpdateModel(Product Model)
        {
             var data = updateModel(Model);
            return data;
        }
    }
}
