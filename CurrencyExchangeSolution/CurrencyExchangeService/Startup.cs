using Castle.Windsor.Installer;
using Castle.Windsor;
using System.Reflection;

namespace CurrencyExchange.Service
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For demo purposes make the swagger interface available on all environments
            app.UseSwagger();
            app.UseSwaggerUI();

            // Set up Attribute Based Routing
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
                       

            //app.UseExceptionHandler(options =>
            //{
            //    options.Run(async context =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        context.Response.ContentType = "text/html";
            //    })
            //})
        }

        public void ConfigureServices(IServiceCollection services, IWindsorContainer container)
        {
            // Register Controllers
            services.AddControllers();
            // Set up Swagger Services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
                setup.UseAllOfToExtendReferenceSchemas()   
            );

            // Register all services in installers within this assembly
            container.Install(FromAssembly.Instance(Assembly.GetCallingAssembly()));
        }
    }
}
