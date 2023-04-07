using PMS.Data;
using PMS.Models.Models;
using PMS.Services.IServices;

namespace PMS.Services.Services
{
    internal class FileService :IFileService
    {
        readonly FileUploadClass _fileService;
        public FileService(FileUploadClass fileService)
        {

            _fileService = fileService;

        }
        public bool DeleteFile(string Image)
        {
            var data = _fileService.FileDelete(Image);
            return data;
        }

        public string UploadFile(Product pro)
        {
            var data =  _fileService.FileUpload(pro);

            return data;
        }
    }
}
