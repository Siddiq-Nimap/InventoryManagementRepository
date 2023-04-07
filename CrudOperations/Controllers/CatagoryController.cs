using AutoMapper;
using PMS.CustomFilters;
using PMS.Services;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMS.Services.IServices;

namespace PMS.Controllers
{

    [Authorize]
    [CustomFilterClass]
    public class CatagoryController : Controller
    {
        readonly ICatagoryService _catagoryService;
        readonly ICredentialService _credentialService;
        readonly ICatagoryOpService _catagoryOpService;
        public CatagoryController(ICatagoryService CatagoryService,ICredentialService Credentialservice,ICatagoryOpService CatagoryOpService)
        {
            _catagoryService = CatagoryService;
            _credentialService = Credentialservice;
            _catagoryOpService = CatagoryOpService;
        }

        [HttpGet]
        
        public ActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CatagoryList(int PagingNbr = 1)
        {
            var data = await _catagoryService.Paging(PagingNbr);

            ViewBag.CurrentPage = PagingNbr;

            ViewBag.TotalPage = _catagoryService.TotalPages();
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
            var id = await _credentialService.GetIdByUsername(name);

            var data = await _catagoryOpService.GetReportByIdAsync(id);
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ReportALL()
        {
            var data = await _catagoryOpService.GetReportAsync();
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

            _catagoryService.InsertModel(cata);
            bool data =  await _catagoryService.Save();
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
            var data = await _catagoryOpService.GetProductsByCatagoryIdAsync(id);
            var ProductList = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(data);
            return View(ProductList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ActionName("AddProduct")]
        public async Task<ActionResult> AddProductAsync(int id)
        {
            TempData["id"] = id;
            var data = await _catagoryOpService.GetNonAddedProduct(id);
            var ProductList = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(data);
            return View(ProductList);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("AddProductTo")]
        public async Task<ActionResult> AddProductAsync(int Prodid, int Cataid)
        {
           bool data =  await _catagoryOpService.AddProductInCatagoryAsync(Prodid, Cataid);
            
            if (data == true)
            {
                return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
            else
            {
             ViewBag.ProductInsertCatagory = "Your Product is not inserted in the catagory";
             return RedirectToAction("AddProduct", "Catagory", new {id = Cataid});
            }
        }

        [Authorize(Roles = "Admin")]
        [ActionName("DeActivate")]
        public async Task<ActionResult> DeActivateAsync(int id)
        {
            await _catagoryOpService.DeActivateByIdAsync(id);

            return RedirectToAction("CatagoryList","Catagory");
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Activate")]
        public async Task<ActionResult> ActivateAsync(int id)
        {
            await _catagoryOpService.ActivateByIdAsync(id);
            return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditAsync(int Id)
        {
            var data = _catagoryService.GetModelById(Id);

            var Catagorydto = Mapper.Map<Catagory, CatagoryDto>(data);
            return View(Catagorydto);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(CatagoryDto cat)
        {
            var cata = Mapper.Map<CatagoryDto, Catagory>(cat);
            
             _catagoryService.UpdateModel(cata);
            bool data = await _catagoryService.Save();
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
            var data = _catagoryService.GetModelById(id);
            var catagorydto = Mapper.Map<Catagory, CatagoryDto>(data);
            return View(catagorydto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(CatagoryDto cat)
        {
            var cata = Mapper.Map<CatagoryDto, Catagory>(cat);
                 _catagoryService.DeleteModel(cata.Id);
            await _catagoryService.Save();
                return RedirectToAction("CatagoryList", "Catagory");
        }

        [HttpGet]
        [ActionName("Details")]
        public ActionResult DetailsAsync(int id)
        {
            var data = _catagoryService.GetModelById(id);
            var cata = Mapper.Map<Catagory,CatagoryDto>(data);
            return View(cata);
        }
    }
}