using System.Reflection;
using Autofac;
using Gos.Core.Search;
using Gos.Core.Search.Aggregations;
using Gos.Infrastructure.Search;
using Gos.Infrastructure.Search.Aggregations;
using Gos.Infrastructure.Search.Converters;
using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using Gos.Infrastructure.Search.QueryHandlers;
using Gos.ServiceModel.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using OpenSearch.Client;
using Module = Autofac.Module;

namespace Gos.Infrastructure.CompositionRoot
{
    public class InfrastructureModule : Module
    {
        private Assembly InfrastructureAssembly => GetType().Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterCaches(builder);
            RegisterConfiguration(builder);
            RegisterSearch(builder);
        }

        private static void RegisterCaches(ContainerBuilder builder)
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            builder.RegisterInstance(memoryCache).As<IMemoryCache>().SingleInstance();
        }

        private static void RegisterConfiguration(ContainerBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
        }

        private void RegisterSearch(ContainerBuilder builder)
        {
            // Client
            builder.RegisterType<ElasticClientFactory>().SingleInstance();
            builder.Register(
                    c =>
                    {
                        var factory = c.Resolve<ElasticClientFactory>();
                        return factory.CreateClient();
                    })
                .As<IOpenSearchClient>()
                .SingleInstance();

            // Index providers
            builder.RegisterType<IndexProviderFactory>().As<IIndexProviderFactory>().SingleInstance();
            builder.RegisterAssemblyTypes(InfrastructureAssembly)
                .Where(t => typeof(IIndexProvider).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .SingleInstance();

            // Search engine
            builder.RegisterType<ElasticSearchEngine>().As<ISearchEngine>().SingleInstance();

            // Entity-DTO converters
            builder.RegisterType<EsDtoConverterFactory>().As<IEsDtoConverterFactory>().SingleInstance();
            builder.RegisterAssemblyTypes(InfrastructureAssembly)
                .Where(t => typeof(IEsDtoConverter).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .SingleInstance();

            // Query builders
            builder.RegisterType<QueryBuilderFactory>().As<IQueryBuilderFactory>().SingleInstance();
            builder.RegisterAssemblyTypes(InfrastructureAssembly).AsClosedTypesOf(typeof(IQueryBuilder<>)).AsImplementedInterfaces().SingleInstance();

            // Query handlers
            builder.RegisterType<QueryHandlerFactory>().As<IQueryHandlerFactory>().SingleInstance();
            builder.RegisterAssemblyTypes(InfrastructureAssembly).AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces().SingleInstance();

            // Aggregators
            builder.RegisterType<AggregatorFactory>().As<IAggregatorFactory>().SingleInstance();
            builder.RegisterType<DiscourseChannelAggregator>().Keyed<IAggregator>(AggregationType.DiscourseChannel).SingleInstance();
            builder.RegisterType<DiscourseEventAggregator>().Keyed<IAggregator>(AggregationType.DiscourseEvent).SingleInstance();
            builder.RegisterType<DiscourseRegionAggregator>().Keyed<IAggregator>(AggregationType.DiscourseRegion).SingleInstance();
            builder.RegisterType<DiscourseTypeAggregator>().Keyed<IAggregator>(AggregationType.DiscourseType).SingleInstance();
            builder.RegisterType<DiscourseYearAggregator>().Keyed<IAggregator>(AggregationType.DiscourseYear).SingleInstance();
            builder.RegisterType<SpeakerAgeAggregator>().Keyed<IAggregator>(AggregationType.SpeakerAge).SingleInstance();
            builder.RegisterType<SpeakerEducationAggregator>().Keyed<IAggregator>(AggregationType.SpeakerEducation).SingleInstance();
            builder.RegisterType<SpeakerLanguageAggregator>().Keyed<IAggregator>(AggregationType.SpeakerLanguage).SingleInstance();
            builder.RegisterType<SpeakerRegionAggregator>().Keyed<IAggregator>(AggregationType.SpeakerRegion).SingleInstance();
            builder.RegisterType<SpeakerSexAggregator>().Keyed<IAggregator>(AggregationType.SpeakerSex).SingleInstance();
            builder.RegisterType<PartOfSpeechAggregator>().Keyed<IAggregator>(AggregationType.PartOfSpeech).SingleInstance();
            builder.RegisterType<LemmaAggregator>().Keyed<IAggregator>(AggregationType.Lemma).SingleInstance();
        }
    }
}