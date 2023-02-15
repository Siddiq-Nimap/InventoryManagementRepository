using CrudOperations.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using DAL.Models;
using DAL.DTO;
using System.Data.Entity;

namespace CrudOperations.Business_Layer.CategoryCrudOperation
{
    public class Categorys : ICategory
    {
        readonly Context productdb;
        public Categorys(Context productdb)
        {
            this.productdb = productdb;
        }


        public async Task<IEnumerable<Product>> GetNonAddedProduct(int id)
        {
            //List<Product> productlists = new List<Product>();

            //using (SqlConnection Con = new SqlConnection(productdb.cs))
            //{

            //    SqlCommand command = new SqlCommand("spAddTocata", Con);
            //    command.CommandType = CommandType.StoredProcedure;
            //    await Con.OpenAsync();
            //    command.Parameters.AddWithValue("@CatagoryId", id);

            //    SqlDataReader sdr = await command.ExecuteReaderAsync();
            //    while (await sdr.ReadAsync())
            //    {
            //        Product product = new Product();
            //        product.Id = await sdr.GetFieldValueAsync<int>(0);

            //        product.ProductGenericName = await sdr.GetFieldValueAsync<string>(1);

            //        product.ProductTitle = await sdr.GetFieldValueAsync<string>(2);

            //        product.ProductPrice = await sdr.GetFieldValueAsync<int>(3);

            //        product.ImagePath = await sdr.GetFieldValueAsync<string>(4);

            //        productlists.Add(product);
            //    }
            //}

            var data = await productdb.Products.ToListAsync();

            return data;
        }

        public async Task<List<Product>> GetProductsByCatagoryIdAsync(int id)
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

        public async Task<List<ReportDto>> GetReportAsync(int id)
        {
            var param = new SqlParameter("@LoginId", id);

            var data = await productdb.Database.SqlQuery<ReportDto>("execute spReport @LoginId", param).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            var data = await productdb.Database.SqlQuery<ReportDto>("execute sp_Reportall").ToListAsync();
            return data;

        }


    }
}