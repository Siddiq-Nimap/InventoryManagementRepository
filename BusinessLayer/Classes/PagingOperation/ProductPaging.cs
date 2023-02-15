using CrudOperations.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using DAL.Models;

namespace CrudOperations.Business_Layer
{
    public class ProductPaging : IPaging
    {
        readonly Context productdb;
        public ProductPaging(Context productdb)
        {
            this.productdb = productdb;
        }
        public int TotalPage;
        public async Task<T> Paging<T>(int PagingNbr,string returntype)
        {
            var Parameters = new[]
            {
                new SqlParameter("@PagingNbr",PagingNbr),
                new SqlParameter("@TotalPages",SqlDbType.Int){Direction = ParameterDirection.Output}
            };

            if (returntype.StartsWith("List<Product>"))
            {
                var data = await productdb.Products.SqlQuery("execute spGetProductsPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

                TotalPage = (int)Parameters[1].Value;

                return (T)(object)data;

            }
            else
            {
                var data = await productdb.Catagories.SqlQuery("execute spGetCatagoryPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

                TotalPage = (int)Parameters[1].Value;

                return (T)(object)data;
            }
                      
        }

        public int TotalPages()
        {
            return TotalPage;
        }
    }

}