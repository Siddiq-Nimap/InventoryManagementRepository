using AutoMapper;
using BusinessLayer;
using CrudOperations.CustomFilters;
using CrudOperations.Interfaces;
using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CrudOperations.Controllers
{

    [Authorize]
    [CustomFilterClass]
    public class CatagoryController : Controller
    {
        readonly ILogin logins;
        readonly ICategory Category;
        readonly ICategoryInsert categoryInsert;
        readonly ICategoryActivation CategoryActivate;
        readonly IAllRepository<Catagory> catagory;
        readonly IPaging page;
        public CatagoryController(
            ILogin logins,
            ICategoryActivation activate,ICategoryInsert catin,
            IAllRepository<Catagory> cat, ICategory Cata,IPaging page)
        {
            
            this.logins = logins;
            CategoryActivate = activate;
            catagory= cat;
            categoryInsert = catin;
            Category = Cata;
            this.page = page;
        }

        [HttpGet]
        public ActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CatagoryList(int PagingNbr = 1)
        {
            var data = await page.Paging<List<Catagory>>(PagingNbr, "List<Catagory>");

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = page.TotalPages();
            var Catdto = Mapper.Map<IEnumerable<Catagory>, IEnumerable<CatagoryDto>>(data);

            return View(Catdto);


        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Report()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity.Claims;

            var IdentifierName = claims.Where(model => model.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            string name = IdentifierName.Value;
            var id = await logins.GetIdByUsername(name);

            var data = await Category.GetReportAsync(id);
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ReportALL()
        {
            var data = await Category.GetReportAsync();
            if(data != null) { return View(data); } else { return RedirectToAction("CatagoryList", "Catagory"); }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
      public async Task<ActionResult> Create(CatagoryDto cat)
        {
           var cata = Mapper.Map<CatagoryDto,Catagory>(cat);

            catagory.InsertModel(cata);
            bool data =  await catagory.Save();
            if (data == true)
            {
                return Json("Your data has successfully inserted");
            }
            else
            {
                ViewBag.Create = "Your Data has not inserted";
                return View();
            }
        }

        [HttpGet]
        [ActionName("ShowProduct")]
        public async Task<ActionResult> ShowProductAsync(int id)
        {
            var data = await Category.GetProductsByCatagoryIdAsync(id);
            var ProductList = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(data);
            return View(ProductList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("AddProduct")]
        public async Task<ActionResult> AddProductAsync(int id)
        {
            TempData["id"] = id;
            var data = await Category.GetNonAddedProduct(id);
            var ProductList = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(data);
            return View(ProductList);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("AddProductTo")]
        public async Task<ActionResult> AddProductAsync(int Prodid, int Cataid)
        {
           bool data =  await categoryInsert.InsertProductInCatagoryAsync(Prodid, Cataid);
            
            if (data == true)
            {
                return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
            else
            {
             ViewBag.ProductInsertCatagory = "Your Produc is not inserted in the catagory";
             return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
        }

        [Authorize(Roles = "Admin")]
        [ActionName("DeActivate")]
        public async Task<ActionResult> DeActivateAsync(int id)
        {
            await CategoryActivate.DeActivateAsync(id);

            return RedirectToAction("CatagoryList","Catagory");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Activate")]
        public async Task<ActionResult> ActivateAsync(int id)
        {
            await CategoryActivate.ActivateAsync(id);
            return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditAsync(int Id)
        {
            //var data = await category.GetCatagoryByIdAsync(Id);
            var data = catagory.GetModelById(Id);

            var Catagorydto = Mapper.Map<Catagory, CatagoryDto>(data);
            return View(Catagorydto);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(CatagoryDto cat)
        {
            //bool data = await CategoryModify.EditCatagoryAsync(cat);


            var cata = Mapper.Map<CatagoryDto, Catagory>(cat);
            
             catagory.UpdateModel(cata);
            bool data = await catagory.Save();
            if (data == true)
            {
                return RedirectToAction("CatagoryList", "Catagory");
            }
            else
            {
                ViewBag.EditMessage = "Your data has not been edited";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            //var data = await category.GetCatagoryByIdAsync(id);
            var data = catagory.GetModelById(id);
            var catagorydto = Mapper.Map<Catagory, CatagoryDto>(data);
            return View(catagorydto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(CatagoryDto cat)
        {
            var cata = Mapper.Map<CatagoryDto, Catagory>(cat);
                 catagory.DeleteModel(cata.Id);
            await catagory.Save();
                return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Details")]
        public ActionResult DetailsAsync(int id)
        {
            //var data = await category.GetCatagoryByIdAsync(id);
            var data = catagory.GetModelById(id);
            var cata = Mapper.Map<Catagory,CatagoryDto>(data);
            return View(cata);
        }
    }
}