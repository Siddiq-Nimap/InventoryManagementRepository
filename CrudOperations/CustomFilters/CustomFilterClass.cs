using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.CustomFilters
{
    public class CustomFilterClass :FilterAttribute ,IExceptionFilter
    {


        public void OnException(ExceptionContext filterContext)
        {
            string message = filterContext.RouteData.Values["Controller"].ToString() + "->" +
                filterContext.RouteData.Values["Action"].ToString() + " -> " + 
                filterContext.Exception.Message +"\n"+filterContext.Exception.InnerException + "\t -->" + DateTime.Now.ToString() + "\n";

            ExecutionError(message);
            ExecutionError("\n-------------------------------------------------------------------------------------------------------------------------\n\n");
        }

        private void ExecutionError(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/ErrorLog/Error.txt"),data);
        }
    }
}