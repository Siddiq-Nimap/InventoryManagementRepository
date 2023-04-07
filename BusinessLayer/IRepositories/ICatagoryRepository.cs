
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Data.IRepository
{
    public interface ICatagoryRepository 
    {

        Task<List<Product>> ProductsByCatagoryIdAsync(int id);

        Task<List<ReportDto>> ReportByIdAsync(int id);
        Task<IEnumerable<ReportDto>> ReportAsync();

        Task<bool> De_ActivateAsync(int id);

        Task<bool> ActivateByIdAsync(int id);
        Task<bool> InsertProductInCatagoryAsync(int productId, int CatagoryId);
        Task<IEnumerable<Product>> GetNonAddedProduct(int id);
        Task<T> Paging<T>(int PaginNbr, string returntype);

        int TotalPages();



    }
}
