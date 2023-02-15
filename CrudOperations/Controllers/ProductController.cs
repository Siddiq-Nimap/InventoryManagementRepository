using AutoMapper;
using BusinessLayer;
using CrudOperations.CustomFilters;
using CrudOperations.Interfaces;
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CrudOperations.Controllers
{
    [Authorize]
    [CustomFilterClass]
   
    public class ProductController : Controller
    {
        readonly IFileSaving file;
        readonly ILogin logins;
        readonly IAllRepository<Product> product;
        readonly IPaging page;
        public ProductController(
            IFileSaving file,
            ILogin logins,
           IAllRepository<Product> Pro,
           IPaging page)
        {
            this.file = file;
            this.logins = logins;
            product = Pro;
            this.page = page;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int PagingNbr = 1)
        {
            var data = await page.Paging<List<Product>>(PagingNbr, "List<Product>");

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = page.TotalPages();

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

            var data = await logins.GetIdByUsername(name);

            Session["userid"] = data;
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Create")]
        
        public async Task<ActionResult> CreateAsync(ProductDto fruits)
        {
            var fruit = Mapper.Map<ProductDto, Product>(fruits);

            string data = file.FileUpload(fruit);
            if (data.StartsWith("~/Images/"))
            {
                fruit.ImagePath = data;

                //bool Check = await ProductInsert.InsertProductAsync(fruits);
                product.InsertModel(fruit);

                bool check = await product.Save();

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
            var data = product.GetModelById(Id);
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
                string data = file.FileUpload(fruit);

                if (data.StartsWith("~/Images/"))
                {
                    fruit.ImagePath = data;

                    //bool check = await ProductModify.EditProductAsync(fruits);
                    product.UpdateModel(fruit);
                    bool check = await product.Save();

                    if (check == true)
                    { 
                        file.FileDelete(Session["Image"].ToString());
                        return Json("Your Data has been successfully Edited");
                    }
                    else{ return Json("Your Data has not Edited"); }
                }
                else{ return Json(data); }
            }
            else
            {
                fruit.ImagePath = Session["Image"].ToString();
                //bool check = await ProductModify.EditProductAsync(fruits);
               product.UpdateModel(fruit);
                bool check = await product.Save();

                if (check == true){return Json("Your Data has been updated successfully");}
                else{ return Json("Your Data has not Edited");}
            }
            
        }

        [HttpGet]
       
        public ActionResult Delete(int id)
        {
            //var data = await products.GetProductByIdAsync(id);

            var data = product.GetModelById(id);

            TempData["Images"] = data.ImagePath;

            var prodto = Mapper.Map<Product, ProductDto>(data);
            return View(prodto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(ProductDto prod)
        {

            //var data = await ProductModify.DeleteProductAsync(pro.Id);
            var pro = Mapper.Map<ProductDto, Product>(prod);
             product.DeleteModel(pro.Id);
            var data = await product.Save();
            if (data == true)
            {
                file.FileDelete(TempData["Images"].ToString());
                return RedirectToAction("Index", "Product");
            }
            else{return RedirectToAction("Delete","Product",new {id = pro.Id});}
        }

        [HttpGet]
      
        public ActionResult Details(int Id)
        {
            //var data = await products.GetProductByIdAsync(Id);
            var data = product.GetModelById(Id);
           

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