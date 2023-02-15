using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DAL.DTO
{
    public class ProductDto
    {

            public int Id { get; set; }

            [Required(ErrorMessage = "Product Generic Name is required ")]
            [DisplayName("Generic Name")]
            public string ProductGenericName { get; set; }


            [Required(ErrorMessage = "Product Description is required ")]
            [DisplayName("Description")]
            public string ProductDescription { get; set; }


            [Required(ErrorMessage = "Title is required ")]
            [DisplayName("Title")]
            public string ProductTitle { get; set; }


            [Required(ErrorMessage = "Product Weight is required ")]
            [DisplayName("Weight")]
            public int ProductWeight { get; set; }


            [Required(ErrorMessage = "Product price is required ")]
            [DisplayName("Price")]
            public int ProductPrice { get; set; }


            [Required(ErrorMessage = "Please Upload Image Name is required ")]
            [DisplayName("Choose Image")]

            public string ImagePath { get; set; }

            
            public HttpPostedFileBase ImageFile { get; set; }

            
            public int UserID { get; set; }

            

        
    }
}
