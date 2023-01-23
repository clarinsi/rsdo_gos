using System.Diagnostics;
using Autofac;
using Gos.Core.Interfaces;
using Gos.Web.SelectLists;
using Gos.Web.Sessions;

namespace Gos.Web.CompositionRoot
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterSelectListProviders(builder);
            RegisterSessions(builder);
        }

        private static void RegisterSelectListProviders(ContainerBuilder builder)
        {
            builder.RegisterType<SelectListProviderFactory>().As<ISelectListProviderFactory>().SingleInstance();
            builder.RegisterType<ConditionSelectListProvider>().Keyed<ISelectListProvider>(SelectListType.Condition).SingleInstance();
            builder.RegisterType<ListSortSelectListProvider>().Keyed<ISelectListProvider>(SelectListType.ListSort).SingleInstance();
            builder.RegisterType<PartOfSpeechSelectListProvider>().Keyed<ISelectListProvider>(SelectListType.PartOfSpeech).InstancePerLifetimeScope();
            builder.RegisterType<MarkSelectListProvider>().Keyed<ISelectListProvider>(SelectListType.Mark).SingleInstance();
            builder.RegisterType<WordSelectListProvider>().Keyed<ISelectListProvider>(SelectListType.Word).SingleInstance();
        }

        private static void RegisterSessions(ContainerBuilder builder)
        {
            builder.RegisterType<SessionIdResolver>().As<ISessionIdResolver>().SingleInstance();
            builder.RegisterType<TraceIdentifierResolver>().As<ITraceIdentifierResolver>().SingleInstance();
        }
    }
}