using AutoMapper;
using CrudOperations.App_Start;
using CrudOperations.Business_Layer;
using CrudOperations.Interfaces;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CrudOperations
{
    public class MvcApplication : HttpApplication
    {
        readonly IAuthenticationManager Authenticate;
        public MvcApplication()
        {
           Authenticate = new AuthenticationClass();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
        protected void Application_AuthenticateRequest()
        {
            var token = Request.Cookies["Bearer"]?.Value;

            if (token != null)
            {
                
               var principal = Authenticate.ValidationToken(token);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}
