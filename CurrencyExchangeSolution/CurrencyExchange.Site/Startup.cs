using Castle.Windsor.Installer;
using Castle.Windsor;
using System.Reflection;
using ExchangeBureau;

namespace CurrencyExchange.Site
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services, IWindsorContainer container)
        {

            // Register Service Clients into Services Collection 
            // TODO: Look at ways to do this in WindsorInstallers
            services.AddHttpClient<ICurrencyExchangeClient, CurrencyExchangeClient>(
                (provider, client) => {
                    client.BaseAddress = new Uri(_configuration.GetValue("ExchangeService:RootUri", "https://localhost:7061/"));
                });
      

            // Register all services in installers within this assembly
            container.Install(FromAssembly.Instance(Assembly.GetCallingAssembly()));

            // Register controllers to the DI container using the in built services locator interface
            services.AddControllersWithViews();
        }
    }
}
