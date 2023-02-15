using System.Web.Mvc;
using Unity;
using System.Web.Http;
using CrudOperations.Business_Layer.CategoryCrudOperation;
using CrudOperations.Interfaces;
using CrudOperations.Business_Layer.Activation;
using CrudOperations.Business_Layer.Insertion;
using CrudOperations.Business_Layer;
using BusinessLayer;

namespace CrudOperations
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
            container.RegisterType<ICategory, Categorys>();
            container.RegisterType<ICategoryActivation, Activation>();
            container.RegisterType<ICategoryInsert, CategoryInsertion>();

            //Product Resolver
            container.RegisterType(typeof(IAllRepository<>),typeof(AllRepository<>));

            //Login Resolver
            container.RegisterType<ILogin, CredentialClass>();

            //File Resolver
            container.RegisterType<IFileSaving, FileUploadClass>();

            //Paging Resolver
            container.RegisterType<IPaging, ProductPaging>();

            //Authentication Resolver
            container.RegisterType<IAuthenticationManager, AuthenticationClass>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}