using Autofac;
using Gos.Core.Interfaces;
using Gos.Infrastructure.Sessions;

namespace Gos.Console.CompositionRoot;

public class ConsoleModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DefaultSessionIdResolver>().As<ISessionIdResolver>().SingleInstance();
        builder.RegisterType<DefaultTraceIdentifierResolver>().As<ITraceIdentifierResolver>().SingleInstance();
    }
}