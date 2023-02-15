using CrudOperations.Interfaces;
using DAL.DTO;
using DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperations.Business_Layer
{
    public class CredentialClass : ILogin
    {
        readonly Context productdb;
        public CredentialClass()
        {
            productdb = new Context();
        }
        public async Task<int> GetIdByUsername(string username)
        {
            var data = await productdb.Logins.SingleAsync(model => model.UserName == username);

            return data.Id;
        }

        public async Task<Logins> LoginEntAsync(LoginDto user)
        {
            var credentials = await productdb.Logins.FirstOrDefaultAsync(model => model.UserName == user.UserName && model.Password == user.Password);
            if (credentials == null){return null;}
            else{return credentials;}
        }
    }
}