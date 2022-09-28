using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExchangeEngine;

namespace CurrencyExchange.Site.Installers
{
    public class ModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //var service = new CurrencyExchangeApi( ["ExchangeServiceUri"])
            //var serviceConfig = container.Resolve<ExchangeServiceConfiguration>();

            //container.Register(Component
            //    .For<CurrencyExchangeApi>()
            //    .ImplementedBy<CurrencyExchangeApi>()
            //    .DependsOn(Dependency.OnValue("")
            //    .LifestyleSingleton
            //    )

            //throw new NotImplementedException();
        }
    }
}
