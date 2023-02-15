using AutoMapper;
using BusinessLayer;
using CrudOperations.Interfaces;
using DAL.DTO;
using DAL.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CrudOperations.Controllers
{
    public class LoginController : Controller
    {
        readonly ILogin logins;
        readonly IAuthenticationManager Authenticate;
        readonly IAllRepository<Logins> Log;
        public LoginController(ILogin logins, IAuthenticationManager authenticate,IAllRepository<Logins> log)
        {
            this.logins = logins;
            Authenticate = authenticate;
            Log = log;
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
                var data = await logins.LoginEntAsync(login);
                if (data != null)
                {

                    var token = Authenticate.GenerateToken(data);

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

                 Log.InsertModel(sign);
                bool data = await Log.Save();
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