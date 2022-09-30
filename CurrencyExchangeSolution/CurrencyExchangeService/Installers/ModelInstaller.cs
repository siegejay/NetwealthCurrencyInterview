using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using CurrencyExchange.Model;
using System.Reflection;

namespace CurrencyExchange.Service.Installers
{
    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<ICurrency>()
                    .WithService
                    .FromInterface(typeof(ICurrency))
                    .Configure(c => c.Named(c.Implementation.Name))
            );

            container.Register(Component
                .For<ICurrencyProvider>()
                .ImplementedBy<BasicCurrencyProvider>()
                .LifestyleSingleton()
            );

            container.Register(
                Types.FromAssembly(Assembly.GetExecutingAssembly())
                    .BasedOn<IExchangeRate>()
                    .WithService
                    .FromInterface(typeof(IExchangeRate))
                    .Configure(c => c.Named(c.Implementation.Name))
            );

            container.Register(
                Component.For<IExchangeRateProvider>().ImplementedBy<SameFromToExchangeRateProviderDecorator>(),
                Component.For<IExchangeRateProvider>().ImplementedBy<BasicExchangeRateProvider>()
                .LifestyleSingleton()
            );
            
            container.Register(Component
                .For<IExchangeBureau>()
                .ImplementedBy<ExchangeBureau>()
                .LifeStyle.ScopedToNetServiceScope()                
            );


        }
    }
}
