using AutoMapper;
using BusinessLayer.IRepositories;
using BusinessLayer.Repositories;
using PMS.App_Start;
using PMS.Services.IServices;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PMS
{
    public class MvcApplication : HttpApplication
    {
        readonly ICredentialManager _credentialService;
        public MvcApplication()
        {
            _credentialService = new CredentialManager();
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
                
               var principal = _credentialService.ValidationToken(token);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }
    }
}
