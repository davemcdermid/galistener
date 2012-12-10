namespace GAListener
{
	using System.Reflection;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Autofac;
	using Autofac.Integration.Mvc;
	using GAListener.Repository;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute("utm", "__utm.gif", new { controller = "Utm", action = "RecordHit" });
			routes.MapRoute("clearhits", "clearall", new { controller = "Utm", action = "Clear" });
			routes.MapRoute("hits", "", new { controller = "Utm", action = "GetHits" });

		}

		protected void Application_Start()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
			builder.RegisterModelBinderProvider();

			builder.RegisterType<HitRepository>().As<IHitRepository>().SingleInstance();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}