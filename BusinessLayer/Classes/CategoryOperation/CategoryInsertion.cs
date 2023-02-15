using CrudOperations.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using DAL.Models;

namespace CrudOperations.Business_Layer.Insertion
{
    public class CategoryInsertion : ICategoryInsert
    {
        readonly Context productdb;
        public CategoryInsertion(Context productdb)
        {
            this.productdb = productdb;
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
                if (a > 0){return true;}else{return false;}
            }
        }

    }
}