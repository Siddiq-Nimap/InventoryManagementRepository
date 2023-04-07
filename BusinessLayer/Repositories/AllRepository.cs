using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PMS.Data.IRepository;
using PMS.Models.Models;

namespace PMS.Data
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

        public bool deleteModel(int id)
        {
            T model = DbEntity.Find(id);
            
          var check =  DbEntity.Remove(model);
            if (check != null){return true;}
            else { return false; }

        }

        public async Task<IEnumerable<T>> getModel()
        {
            return await DbEntity.ToListAsync();

        }

        public T getModelById(int id)
        {
            T model = DbEntity.Find(id);

            return model;
        }

        public bool insertModel(T Model)
        {
            var a =  DbEntity.Add(Model);
            if (a != null)
            {
                return true;
            }
            else { return false; }

        }

        public async Task<bool> save()
        {
         var a =  await ContextDB.SaveChangesAsync();

            if (a > 0)
            {
                return true;
            }
            return false;
        }

        public bool updateModel(T Model)
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
