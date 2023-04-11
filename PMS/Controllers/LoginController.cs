using AutoMapper;
using BusinessLayer.IRepositories;
using PMS.Services;
using PMS.Services.IServices;
using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class LoginController : Controller
    {
        readonly IProductService _productService;
        
        readonly ICredentialService _credentialService;
        
        public LoginController(IProductService productService,ICredentialService credentialService)
        {
         _productService = productService;
            _credentialService = credentialService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                var data = await _credentialService.LoginEntAsync(login);
                if (data != null)
                {

                    var token =     _credentialService.GetToken(data);

                    Response.Cookies.Set(new HttpCookie("Bearer", token));
                    return RedirectToAction("Index", "Catagory");
                }
                else { ViewBag.LoginError = "Logins Failed"; }
            }
            return View();
        }
    
 
        public ActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task< ActionResult> SignUpAsync(SignUpDto signup)
        {
            if(ModelState.IsValid)
            {
                var sign = Mapper.Map<SignUpDto, Logins>(signup);

                 _credentialService.InsertModel(sign);
                bool data = await _credentialService.Save();
                if (data == true)
                {
                    ViewBag.LoginMessage = "Your Account has been created";
                    ModelState.Clear();
                }
                else{ViewBag.LoginMessage = "Your Account has not create";}
            }
            return View();
        }

        public ActionResult Logout()
        {
           var cookie = Request.Cookies["Bearer"];

            cookie.Expires = DateTime.Now.AddSeconds(1);

            Response.Cookies.Add(cookie);
            return View("Index");
        }
    }
}