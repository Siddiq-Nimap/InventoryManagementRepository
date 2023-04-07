using AutoMapper;
using PMS.CustomFilters;
using PMS.Services;
using PMS.Services.IServices;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMS.Controllers
{
    [Authorize]
    [CustomFilterClass]
   
    public class ProductController : Controller
    {
        readonly IProductService _productService;
        readonly ICredentialService _credentialService;
        readonly IFileService _fileService;
        public ProductController(IProductService productService , ICredentialService credentialService,IFileService fileService)
        {
            _productService = productService;
            _credentialService = credentialService;
            _fileService = fileService;
            
        }

        [HttpGet]
        public async Task<ActionResult> Index(int PagingNbr = 1)
        {
            var data = await _productService.Paging(PagingNbr);

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = _productService.TotalPages();

            var Prodto = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(data);

            return View(Prodto);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity.Claims;

            var IdentifierName = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string name = IdentifierName.Value;

            var data = await _credentialService.GetIdByUsername(name);

            Session["userid"] = data;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Create")]
        
        public async Task<ActionResult> CreateAsync(ProductDto fruits)
        {
            var fruit = Mapper.Map<ProductDto, Product>(fruits);

            string data = _fileService.UploadFile(fruit);
            if (data.StartsWith("~/Images/"))
            {
                fruit.ImagePath = data;

                //bool Check = await ProductInsert.InsertProductAsync(fruits);
                _productService.InsertModel(fruit);

                bool check = await _productService.Save();

                if (check == true){return Json("Your Data has been inserted successfully");}
                else{return Json("Your Data has not inserted");}
            }
            else
            {
                return Json(data);
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            //var data = await products.GetProductByIdAsync(Id);
            var data = _productService.GetModelById(Id);
            Session["Image"] = data.ImagePath;

            var Prodto = Mapper.Map<Product, ProductDto>(data);
            return View(Prodto);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        
        public async Task<ActionResult> EditAsync(ProductDto fruits)
        {
            var fruit = Mapper.Map<ProductDto , Product>(fruits);

            if (fruit.ImageFile != null)
            {
                string data = _fileService.UploadFile(fruit);

                if (data.StartsWith("~/Images/"))
                {
                    fruit.ImagePath = data;

                    _productService.UpdateModel(fruit);
                    bool check = await _productService.Save();

                    if (check == true)
                    { 
                        _fileService.DeleteFile(Session["Image"].ToString());
                        return Json("Your Data has been successfully Edited");
                    }
                    else{ return Json("Your Data has not Edited"); }
                }
                else{ return Json(data); }
            }
            else
            {
                fruit.ImagePath = Session["Image"].ToString();
               _productService.UpdateModel(fruit);
                bool check = await _productService.Save();

                if (check == true){return Json("Your Data has been updated successfully");}
                else{ return Json("Your Data has not Edited");}
            }
            
        }

        [HttpGet]
       
        public ActionResult Delete(int id)
        {

            var data = _productService.GetModelById(id);

            TempData["Images"] = data.ImagePath;

            var prodto = Mapper.Map<Product, ProductDto>(data);
            return View(prodto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(ProductDto prod)
        {

            var pro = Mapper.Map<ProductDto, Product>(prod);
             _productService.DeleteModel(pro.Id);
            var data = await _productService.Save();
            if (data == true)
            {
                _fileService.DeleteFile(TempData["Images"].ToString());
                return RedirectToAction("Index", "Product");
            }
            else{return RedirectToAction("Delete","Product",new {id = pro.Id});}
        }

        [HttpGet]
      
        public ActionResult Details(int Id)
        {
            var data = _productService.GetModelById(Id);
           

            if (data == null){return RedirectToAction("Index", "Catagory");}
            else
            {
                var prodto = Mapper.Map<Product,ProductDto>(data);
                return View(prodto);
            }
        }
        
        public ActionResult Pages()
        {
            return View();
        }
    }
}