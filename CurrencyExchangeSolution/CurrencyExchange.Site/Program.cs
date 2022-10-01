namespace CurrencyExchange.Site
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
            

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services, container);
            var app = builder.Build(); 
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();

            startup.Configure(app);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}