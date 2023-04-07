using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using PMS.Models.Models.DTO;
using PMS.Models.Models;
using PMS.Data.IRepository;

namespace PMS.Data
{
    public class CatagoryRepository : ICatagoryRepository
    {
        readonly Context productdb;
        public CatagoryRepository(Context productdb)
        {
            this.productdb = productdb;
        }
        public int TotalPage;

        public async Task<List<Product>> ProductsByCatagoryIdAsync(int id)
        {
            List<Product> productlists = new List<Product>();

            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {

                SqlCommand command = new SqlCommand("spGetCatagoriesProduct", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@CatagoryId", id);

                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    Product product = new Product();
                    product.Id = await sdr.GetFieldValueAsync<int>(0);

                    product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);

                    product.ProductTitle = await sdr.GetFieldValueAsync<string>(2);

                    product.ProductPrice = await sdr.GetFieldValueAsync<int>(3);

                    product.ImagePath = await sdr.GetFieldValueAsync<string>(4);

                    productlists.Add(product);
                }
            }
            return productlists;
        }

        public async Task<List<ReportDto>> ReportByIdAsync(int id)
        {
            var param = new SqlParameter("@LoginId", id);

            var data = await productdb.Database.SqlQuery<ReportDto>("execute spReport @LoginId", param).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ReportDto>> ReportAsync()
        {
            var data = await productdb.Database.SqlQuery<ReportDto>("execute sp_Reportall").ToListAsync();
            return data;

        }

        public async Task<bool> ActivateByIdAsync(int id)
        {

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand Command = new SqlCommand("spActivator", con);
                Command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                Command.Parameters.AddWithValue("@CatagoryId", id);

                int a = await Command.ExecuteNonQueryAsync();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }

        public async Task<bool> De_ActivateAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand Command = new SqlCommand("spDeactivate", con);
                Command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();

                Command.Parameters.AddWithValue("@CatagoryId", id);

                int a = await Command.ExecuteNonQueryAsync();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }

        public async Task<bool> InsertProductInCatagoryAsync(int productId, int CatagoryId)
        {

            using (SqlConnection con = new SqlConnection(productdb.cs))
            {
                SqlCommand command = new SqlCommand("spAddProduct", con);
                command.CommandType = CommandType.StoredProcedure;
                await con.OpenAsync();
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@CatagoryId", CatagoryId);

                int a = await command.ExecuteNonQueryAsync();
                if (a > 0) { return true; } else { return false; }
            }
        }
        public async Task<IEnumerable<Product>> GetNonAddedProduct(int id)
        {
            List<Product> productlists = new List<Product>();

            using (SqlConnection Con = new SqlConnection(productdb.cs))
            {

                SqlCommand command = new SqlCommand("spAddTocata", Con);
                command.CommandType = CommandType.StoredProcedure;
                await Con.OpenAsync();
                command.Parameters.AddWithValue("@CatagoryId", id);

                SqlDataReader sdr = await command.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    Product product = new Product();
                    product.Id = await sdr.GetFieldValueAsync<int>(0);

                    product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);

                    product.ProductTitle = await sdr.GetFieldValueAsync<string>(2);

                    product.ProductPrice = await sdr.GetFieldValueAsync<int>(3);

                    product.ImagePath = await sdr.GetFieldValueAsync<string>(4);

                    productlists.Add(product);
                }
                return productlists;
            }

        }
        public async Task<T> Paging<T>(int PagingNbr, string returntype)
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