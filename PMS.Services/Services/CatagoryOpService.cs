using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PMS.Data;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using PMS.Services.IServices;

namespace PMS.Services.Services
{
    public class CatagoryOpService : ICatagoryOpService
    {
        readonly CatagoryRepository _Catagory;

        public CatagoryOpService(CatagoryRepository Catagory)
        {
            _Catagory = Catagory;
        }

        public async Task<bool> ActivateByIdAsync(int id)
        {
            var data = await _Catagory.ActivateByIdAsync(id);
            return data;
        }

        public async Task<bool> DeActivateByIdAsync(int id)
        {
            var data = await _Catagory.De_ActivateAsync(id);
            return data;
        }


        public async Task<List<Product>> GetProductsByCatagoryIdAsync(int id)
        {
            var data = await _Catagory.ProductsByCatagoryIdAsync(id);
            return data;
        }

        public async Task<List<ReportDto>> GetReportByIdAsync(int id)
        {
            var data = await _Catagory.ReportByIdAsync(id);
            return data;
        }

        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            var data = await _Catagory.ReportAsync();
            return data;
        }

        public async Task<bool> AddProductInCatagoryAsync(int productId, int CatagoryId)
        {
            var data = await _Catagory.InsertProductInCatagoryAsync(productId, CatagoryId);
            return data;
        }

       public async Task<IEnumerable<Product>> GetNonAddedProduct(int id)
        {
            var data = await _Catagory.GetNonAddedProduct(id);
            return data;
        }
    }
}