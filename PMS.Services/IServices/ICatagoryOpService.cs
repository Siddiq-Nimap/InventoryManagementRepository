using PMS.Models.Models.DTO;
using PMS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Services.IServices
{
    public interface ICatagoryOpService
    {
        Task<List<Product>> GetProductsByCatagoryIdAsync(int id);

        Task<List<ReportDto>> GetReportByIdAsync(int id);
        Task<IEnumerable<ReportDto>> GetReportAsync();

        Task<bool> DeActivateByIdAsync(int id);

        Task<bool> ActivateByIdAsync(int id);

        Task<bool> AddProductInCatagoryAsync(int productId, int CatagoryId);

        Task<IEnumerable<Product>> GetNonAddedProduct(int id);



    }
}
