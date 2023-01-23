using Autofac;
using Gos.Infrastructure.CompositionRoot;
using Gos.Services.CompositionRoot;

namespace Gos.Console.CompositionRoot
{
    public class ConsoleBootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new ConsoleModule());
            return builder.Build();
        }
    }
}