﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using ElectronicStore.Data.Core;
using ElectronicStore.Data;
using ElectronicStore.Data.Repositories;
using Autofac;
using ElectronicStore.Service;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using ElectronicStore.Data.Entities;
using System.Web;
using Microsoft.Owin.Security.DataProtection;
using ElectronicStore.Fulcrum;

[assembly: OwinStartup(typeof(ElectronicStore.Web.App_Start.Startup))]

namespace ElectronicStore.Web.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);
            ConfigureAuth(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            //Register WebApi Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbContextFactory>().As<IDbContextFactory>().InstancePerRequest();
            builder.RegisterType<ElectronicStoreDbContext>().AsSelf().InstancePerRequest();

            //Asp.net Identity
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(ProductRepositories).Assembly)
                .Where(t => t.Name.EndsWith("Repositories"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            //Appsetting
            builder.RegisterType<AppSettings>().AsSelf().InstancePerRequest();

            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver
        }
    }
}
