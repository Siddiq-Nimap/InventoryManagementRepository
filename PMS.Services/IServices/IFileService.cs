using PMS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.IServices
{
    public interface IFileService
    {
        string UploadFile(Product pro);

        bool DeleteFile(string Image);
    }
}
