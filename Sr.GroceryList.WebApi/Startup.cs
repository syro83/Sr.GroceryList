using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.ExceptionHandling;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Infra;
using Sr.GroceryList.Infra.http;
using Sr.GroceryList.WebApi.V1.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using MicroElements.Swashbuckle.FluentValidation;
using Sr.GroceryList.Infra.Core;

namespace Sr.GroceryList.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// Add framework services.
			var mvcBuilder = services.AddMvc();
            mvcBuilder.AddNewtonsoftJson();
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			//mvcBuilder.AddFluentValidation();
			mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ListItemValidator>());

			services.AddHttpContextAccessor();

			// add the versioned api explorer, which also adds IApiVersionCreatedAtProvider service
			// note: the specified format code will format the version as "'v'major[.minor][-status]"
			services.AddVersionedApiExplorer(
				options =>
				{
					options.GroupNameFormat = "'v'VVV";

					// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
					// can also be used to control the format of the API version in route templates
					options.SubstituteApiVersionInUrl = true;
				});

			services.AddApiVersioning(o => o.ReportApiVersions = true);

			services.AddSingleton<IConfiguration>(Configuration);

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(
				options =>
				{
					// integrate xml comments
					options.IncludeXmlComments(GetXmlCommentsPath());
					options.DescribeAllEnumsAsStrings();

					// resolve the IApiVersionDescriptionProvider service
					// note: that we have to build a temporary service provider here because one has not been created yet
					var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

					// add a swagger document for each discovered API version
					// note: you might choose to skip or document deprecated API versions differently
					foreach (var description in provider.ApiVersionDescriptions)
					{
						options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
					}

                    // add a custom operation filter which sets default values
                    //options.OperationFilter<Infra.SwaggerDefaultValues>();

                    // ToDo Sr does not yet work
                    //options.AddFluentValidationRules(); // Throws Exception
                    //Doing the AddFluentValidationRules' calls manually, no exception
                    options.SchemaFilter<FluentValidationRules>();
                    options.OperationFilter<FluentValidationOperationFilter>();
                });

			// profiling
			services.AddMiniProfiler(options =>
			   options.RouteBasePath = "/profiler"
			);

			// DI
			services.AddSingleton<IConnectionSettings, EnvironmentVariableConnectionSettings>();
			services.AddSingleton<ICoreContext, CoreContext>();
			services.AddSingleton<IUserContext, CoreHttpContextUserContext>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddSingleton<IListItemRepository, ListItemRepository>();

			services.AddTransient<IValidator<ListItem>, ListItemValidator>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting(routes =>
            {
                routes.MapApplication();
            });

            app.UseAuthorization();

            app.UseSwagger();

			if (env.IsDevelopment())
			{
				app.UseSwaggerUI(
					options =>
					{
						// build a swagger endpoint for each discovered API version
						foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(avd => avd.ApiVersion))
						{
							options.SwaggerEndpoint(Configuration["Appsettings:VirtualDirectory"] + $"swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
						};

						// this custom html has miniprofiler integration
						options.IndexStream = () => GetSwaggerIndex();
					});

				// profiling
				app.UseMiniProfiler();
			}

			// exception handling
			//app.UseMiddleware<ExceptionHandler>();

		}

		#region Helpers


		private string GetXmlCommentsPath()
		{
			//var app = System.AppContext.BaseDirectory;
			//return System.IO.Path.Combine(app.ApplicationBasePath, "Sr.GroceryList.WebApi.xml");
			//var basePath = PlatformServices.Default.Application.ApplicationBasePath;
			//var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
			//return Path.Combine(basePath, fileName);
			return System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Sr.GroceryList.WebApi.xml");
		}

		private Stream GetSwaggerIndex()
		{
			//GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Sr.GroceryList.WebApi.SwaggerIndex.html");
			FileStream stream = File.Open(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Sr.GroceryList.WebApi.SwaggerIndex.html"), FileMode.Open, FileAccess.Read);
			return stream;
		}

		private static Info CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new Info()
			{
				Title = $"ListItem API {description.ApiVersion}",
				Version = description.ApiVersion.ToString(),
				Description = "ListItem.",
				Contact = new Contact() { Name = "Sybren Roede", Email = "sroede@gmail.com" },
				TermsOfService = "Copyright"
			};

			if (description.IsDeprecated)
			{
				info.Description += " This API version has been deprecated.";
			}

			return info;
		}

		#endregion Helpers
	}
}
