using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BusinessLayer
{
    public class AllRepository<T> : IAllRepository<T> where T : class
    {
        readonly Context ContextDB;
        readonly IDbSet<T> DbEntity;

        public AllRepository(Context contextDB)
        {
            ContextDB = contextDB;
            DbEntity = ContextDB.Set<T>();
        }

        public bool DeleteModel(int id)
        {
            T model = DbEntity.Find(id);
            
          var check =  DbEntity.Remove(model);
            if (check != null){return true;}
            else { return false; }

        }

        public async Task<IEnumerable<T>> GetModel()
        {
            return await DbEntity.ToListAsync();

        }

        public T GetModelById(int id)
        {
            T model = DbEntity.Find(id);

            return model;
        }

        public bool InsertModel(T Model)
        {
            var a =  DbEntity.Add(Model);
            if (a != null)
            {
                return true;
            }
            else { return false; }

        }

        public async Task<bool> Save()
        {
         var a =  await ContextDB.SaveChangesAsync();

            if (a > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateModel(T Model)
        {
            var a = ContextDB.Entry(Model).State = EntityState.Modified;

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
