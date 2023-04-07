using PMS.Services.IServices;
using PMS.Data;
using PMS.Models.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using BusinessLayer.IRepositories;

namespace PMS.Services.Services
{
    public class ProductService : IProductService
    {
        readonly Context _productdb;
        readonly IProductRepository _productRepository;
        int TotalPage;
        public ProductService(Context productdb)
        {
            _productdb = productdb;
        }
        public bool DeleteModel(int id)
        {
            var data = _productRepository.DeleteModel(id);
            return data;
        }

        public async Task<IEnumerable<Product>> GetModel()
        {
            var data = await _productRepository.GetModel();
            return data;

        }

        public Product GetModelById(int id)
        {
            var data = _productRepository.GetModelById(id);
            return data;
        }

        public bool InsertModel(Product Model)
        {
            var data = _productRepository.InsertModel(Model);
            return data;
        }

        public async Task<IEnumerable<Product>> Paging(int PagingNbr)
        {
            var Parameters = new[]
           {
                new SqlParameter("@PagingNbr",PagingNbr),
                new SqlParameter("@TotalPages",SqlDbType.Int){Direction = ParameterDirection.Output}
            };
            var data = await _productdb.Products.SqlQuery("execute spGetProductsPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

            TotalPage = (int)Parameters[1].Value;

            return data;

        }

        public async Task<bool> Save()
        {
            var data = await _productRepository.Save();
            return data;
        }

        public int TotalPages()
        {
            return TotalPage;
        }

        public bool UpdateModel(Product Model)
        {
            var data = _productRepository.UpdateModel(Model);
            return data;
        }
    }
}
