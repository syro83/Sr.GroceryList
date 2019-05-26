using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Infra;
using Sr.GroceryList.Wcf.Service;

namespace Sr.GroceryList.Wcf
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			//Bootstrap
			var builder = new ContainerBuilder();

			builder.RegisterType<Service1>();
			builder.RegisterType<ListItemService>();

			builder.RegisterType<Infra.ConfigurationManagerConnectionSettings>().As<IConnectionSettings>();
			builder.RegisterType<Infra.WcfUserContext>().As<IUserContext>();
			builder.RegisterType<CoreContext>().As<ICoreContext>();
			builder.RegisterType<ListItemRepository>().As<IListItemRepository>();


			var container = builder.Build();
			AutofacHostFactory.Container = container;


			/*
			var builder = new ContainerBuilder();
			builder.Register(c => new Infra.ConfigurationManagerConnectionSettings()).As<IConnectionSettings>();
			builder.Register(c => new Infra.WcfUserContext()).As<IUserContext>();
			builder.Register(c => new CoreContext(c.Resolve<IConnectionSettings>(), c.Resolve<IUserContext>())).As<ICoreContext>();
			builder.Register(c => new ListItemRepository(c.Resolve<ICoreContext>())).As<IListItemRepository>();
			builder.Register(c => new ListItemService(c.Resolve<IListItemRepository>())).As<IListItemService>();
			
			builder.Register(c => new Infra.ConfigurationManagerConnectionSettings()).As<IConnectionSettings>();
			builder.Register(c => new Infra.WcfUserContext()).As<IUserContext>().InstancePerLifetimeScope(); 
			builder.Register(c => new CoreContext(c.Resolve<IConnectionSettings>(), c.Resolve<IUserContext>())).As<ICoreContext>().InstancePerLifetimeScope();
			builder.Register(c => new ListItemRepository(c.Resolve<ICoreContext>())).As<IListItemRepository>().InstancePerLifetimeScope();
			builder.Register(c => new ListItemService(c.Resolve<IListItemRepository>())).As<IListItemService>().InstancePerLifetimeScope();
			AutofacHostFactory.Container = builder.Build();
			*/

			RouteTable.Routes.Add(new ServiceRoute("Service", new AutofacServiceHostFactory(), typeof(Service1)));
			RouteTable.Routes.Add(new ServiceRoute("ListItem", new AutofacServiceHostFactory(), typeof(ListItemService)));
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}