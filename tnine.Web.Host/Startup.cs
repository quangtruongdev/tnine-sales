using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Reflection;
using System.Web;
using System.Web.Http;
using tnine.Core;
using tnine.Core.Shared;
using tnine.Core.Shared.Infrastructure;
using tnine.Web.Host.App_Start;

[assembly: OwinStartupAttribute(typeof(tnine.Web.Host.Startup))]
namespace tnine.Web.Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureAuth(app);
        }

        private void ConfigureAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // Register MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterType<DatabaseContext>().AsSelf().InstancePerRequest();

            // Automapper
            var mapperConfiguration = AutoMapperConfig.GetConfiguration();
            builder.RegisterInstance(mapperConfiguration.CreateMapper()).As<IMapper>().SingleInstance();

            mapperConfiguration.AssertConfigurationIsValid();

            // Asp .Net Identity
            builder.RegisterType<UserStore<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>>().As<IUserStore<ApplicationUser, long>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(Assembly.Load("tnine.Core.Shared"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(Assembly.Load("tnine.Application"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            Autofac.IContainer container = builder.Build();
            AutofacDependencyResolver resolver = new AutofacDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
