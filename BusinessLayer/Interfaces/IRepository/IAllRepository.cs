using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IAllRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetModel();
        
        T GetModelById(int id);

        bool InsertModel(T Model);

        bool UpdateModel(T Model);
        
        bool DeleteModel(int id);

        Task<bool> Save();
    }
}
