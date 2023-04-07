using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Data.IRepository
{
    public interface IAllRepository <T> where T : class
    {
        Task<IEnumerable<T>> getModel();
        
        T getModelById(int id);

        bool insertModel(T Model);

        bool updateModel(T Model);
        
        bool deleteModel(int id);

        Task<bool> save();
    }
}
