using System.Web.Mvc;
using Unity;
using System.Web.Http;
using PMS.Services.IServices;
using PMS.Services.Services;
using PMS.Data;
using PMS.Data.IRepository;
using BusinessLayer.Repositories;
using BusinessLayer.IRepositories;

namespace PMS
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //catagory resolver
            container.RegisterType<ICatagoryService, CatagoryService>();
            container.RegisterType<ICatagoryOpService, CatagoryOpService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICatagoryRepository, CatagoryRepository>();
            container.RegisterType <IProductRepository, ProductRepository > ();

            //Login Resolver
            container.RegisterType<ICredentialService, CredentialService>();

            //File Resolver
            container.RegisterType<IFileService, IFileService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}