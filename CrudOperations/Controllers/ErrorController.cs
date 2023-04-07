using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult On404()
        {
            return View();
        }  
        public ActionResult On401()
        {
            return View();
        }   
        public ActionResult On403()
        {
            return View();
        }     
        public ActionResult On500()
        {
            return View();
        }
    }
}