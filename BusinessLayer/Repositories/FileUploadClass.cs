using PMS.Data.IRepository;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
//using Grpc.Core;
using System.IO;
using System.Web;

namespace PMS.Data
{
    public class FileUploadClass : IFileUpload
    {
        public bool FileDelete(string image)
        {

            string oldimg = HttpContext.Current.Request.MapPath(image);
            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);

                return true;
            }
            else{return false;}
        }

        public string FileUpload(Product pro)
        {
            string filename = Path.GetFileNameWithoutExtension(pro.ImageFile.FileName);
            string extention = Path.GetExtension(pro.ImageFile.FileName).ToLower();
            HttpPostedFileBase PostedFile = pro.ImageFile;
            int length = PostedFile.ContentLength;

            if (extention == ".jpg" || extention == ".jpeg" || extention == ".png")
            {
                if (length <= 1000000)
                {
                    filename = filename + extention;

                    pro.ImagePath = "~/Images/" + filename;

                    filename = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), filename);

                    pro.ImageFile.SaveAs(filename);

                    return pro.ImagePath;

                }
                else{return "Max Size is 1Mb";}
            }
            else{return "Supported .jpg , .jpeg, .png";}
        }
    }
}
