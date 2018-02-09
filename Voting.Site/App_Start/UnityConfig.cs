using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Voting.Controllers;
using Voting.Models;
using Voting.IOC;

namespace Voting.App_Start
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            var applicationRegistrar = new ServicesRegistrar();
            applicationRegistrar.RegisterServices(container);
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>()
                .RegisterType<UserManager<ApplicationUser>>()
                .RegisterType<DbContext, ApplicationDbContext>()
                .RegisterType<ApplicationUserManager>()
                .RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}
