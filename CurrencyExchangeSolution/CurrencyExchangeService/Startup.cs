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

            app.UseHttpsRedirection();
            
            app.UseRouting();// Set up Attribute Based Routing
            app.UseAuthorization();

            app.UseCors("AngularPolicy");

            app.UseEndpoints(endpoints => endpoints.MapControllers());            
        }

        public void ConfigureServices(IServiceCollection services, IWindsorContainer container, ConfigurationManager config)
        {
            // Register Controllers
            services.AddControllers();
            // Set up Swagger Services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
                setup.UseAllOfToExtendReferenceSchemas()   
            );

            services.AddCors(options => options.AddPolicy(name: "AngularPolicy", cfg =>
            {
                cfg.AllowAnyHeader();
                cfg.AllowAnyMethod();
                cfg.WithOrigins(config["AllowedCORS"]);
            }));

            // Register all services in installers within this assembly
            container.Install(FromAssembly.Instance(Assembly.GetCallingAssembly()));
        }
    }
}
