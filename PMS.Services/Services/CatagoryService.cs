using PMS.Data;
using PMS.Models.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using PMS.Services.IServices;

namespace PMS.Services.Services
{
    public class CatagoryService : AllRepository<Catagory>, ICatagoryService
    {
        readonly Context productDb;
        public CatagoryService(Context productDb):base(productDb)
        {
            this.productDb = productDb;            
        }
        int TotalPage;
        public bool DeleteModel(int id)
        {
            var data = deleteModel(id);
            return data;
        }

        public async Task<IEnumerable<Catagory>> GetModel()
        {
            var data = await getModel();
            return data;
        }

        public Catagory GetModelById(int id)
        {
            var data = getModelById(id);
            return data;
        }
        public bool InsertModel(Catagory Model)
        {
            var data = insertModel(Model);
            return data;
        }

        public async Task<IEnumerable<Catagory>> Paging(int PagingNbr)
        {
            var Parameters = new[]
           {
                new SqlParameter("@PagingNbr",PagingNbr),
                new SqlParameter("@TotalPages",SqlDbType.Int){Direction = ParameterDirection.Output}
            };
            var data = await productDb.Catagories.SqlQuery("execute spGetCatagoryPaging @PagingNbr = @PagingNbr ,@TotalPages = @TotalPages output", Parameters).ToListAsync();

            TotalPage = (int)Parameters[1].Value;

            return data;
        }

        public async Task<bool> Save()
        {
            var data = await save();
            return data;
        }

        public int TotalPages()
        {
            return TotalPage;
        }

        public bool UpdateModel(Catagory Model)
        {
            var data = updateModel(Model);
            return data;
        }        
    }
}
