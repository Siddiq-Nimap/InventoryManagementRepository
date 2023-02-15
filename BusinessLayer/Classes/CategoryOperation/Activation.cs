using CrudOperations.Interfaces;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using DAL.Models;

namespace CrudOperations.Business_Layer.Activation
{
    public class Activation : ICategoryActivation
    {
        readonly Context productdb;
        public Activation(Context productdb)
        {
            this.productdb = productdb;
        }
        public async Task<bool> ActivateAsync(int id)
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

        public async Task<bool> DeActivateAsync(int id)
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
    }
}