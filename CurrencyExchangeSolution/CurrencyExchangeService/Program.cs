namespace CurrencyExchange.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Explicitly instantiate the DI Container
            var container = new Castle.Windsor.WindsorContainer();

            // Register the Container in place of the internal DI system
            builder.Host.UseWindsorContainerServiceProvider(container);

            // Register Services
            var startup = new Startup();
            startup.ConfigureServices(builder.Services, container, builder.Configuration);
            var app = builder.Build();
            startup.Configure(app);

            app.UseHttpsRedirection();
            app.MapMethods("/api/heartbeat", new[] { "HEAD" }, () => Results.Ok());

            app.Run();
        }
    }
}