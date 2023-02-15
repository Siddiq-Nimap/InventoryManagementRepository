
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudOperations.Interfaces
{
    public interface ICategory
    {

        Task<List<Product>> GetProductsByCatagoryIdAsync(int id);

        Task<List<ReportDto>> GetReportAsync(int id);
        Task<IEnumerable<ReportDto>> GetReportAsync();

        Task<IEnumerable<Product>> GetNonAddedProduct(int id);

    }
}
