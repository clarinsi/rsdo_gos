using System.Reflection;
using Autofac;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Framework;
using Gos.Services.Framework.ConcordanceBuilder;
using Gos.Services.Framework.Decorators;
using Gos.Services.Framework.Fragments;
using Gos.Services.Search.Aggregations;
using Gos.Services.Search.AlternateSearches;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.LemmatizationService;
using Gos.Services.Services.PartOfSpeechService;
using Gos.Services.Services.QueryParser;
using Gos.Services.Services.StatementService;
using MediatR;
using Module = Autofac.Module;

namespace Gos.Services.CompositionRoot
{
    public class ServicesModule : Module
    {
        private Assembly ServicesAssembly => GetType().Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterDatabase(builder);
            RegisterRequestHandlers(builder);
            RegisterServices(builder);
            RegisterFramework(builder);
            RegisterSearch(builder);
        }

        private static void RegisterDatabase(ContainerBuilder builder)
        {
            builder.RegisterType<GosDbContext>().InstancePerLifetimeScope();
        }

        private static void RegisterFramework(ContainerBuilder builder)
        {
            // Fragment parsers
            builder.RegisterType<FragmentParserFactory>().As<IFragmentParserFactory>().SingleInstance();
            builder.RegisterType<CharacterFragmentParser>().Keyed<IFragmentParser>(FragmentType.Character).SingleInstance();
            builder.RegisterType<CorrectedWordFragmentParser>().Keyed<IFragmentParser>(FragmentType.CorrectedWord).SingleInstance();
            builder.RegisterType<CorrectionFragmentParser>().Keyed<IFragmentParser>(FragmentType.Correction).SingleInstance();
            builder.RegisterType<NameFragmentParser>().Keyed<IFragmentParser>(FragmentType.Name).SingleInstance();
            builder.RegisterType<SegmentFragmentParser>().Keyed<IFragmentParser>(FragmentType.Segment).SingleInstance();
            builder.RegisterType<UnclearWordFragmentParser>().Keyed<IFragmentParser>(FragmentType.Unclear).SingleInstance();
            builder.RegisterType<VocalFragmentParser>().Keyed<IFragmentParser>(FragmentType.Vocal).SingleInstance();
            builder.RegisterType<WordFragmentParser>().Keyed<IFragmentParser>(FragmentType.Word).SingleInstance();
            builder.RegisterType<IncidentFragmentParser>().Keyed<IFragmentParser>(FragmentType.Incident).SingleInstance();
            builder.RegisterType<KinesicFragmentParser>().Keyed<IFragmentParser>(FragmentType.Kinesic).SingleInstance();
            builder.RegisterType<PauseFragmentParser>().Keyed<IFragmentParser>(FragmentType.Pause).SingleInstance();
            builder.RegisterType<ForeignFragmentParser>().Keyed<IFragmentParser>(FragmentType.Foreign).SingleInstance();
            builder.RegisterType<GapFragmentParser>().Keyed<IFragmentParser>(FragmentType.Gap).SingleInstance();

            // Concordance builder
            builder.RegisterType<ConcordanceBuilder>().As<IConcordanceBuilder>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<LemmatizationService>().As<ILemmatizationService>().InstancePerLifetimeScope();
            builder.RegisterType<QueryParser>().As<IQueryParser>().SingleInstance();
            builder.RegisterType<PartOfSpeechService>().As<IPartOfSpeechService>().InstancePerLifetimeScope();
            builder.RegisterType<CacheWarmUp>().InstancePerLifetimeScope();
            builder.RegisterType<StatementService>().As<IStatementService>().InstancePerLifetimeScope();
        }

        private void RegisterRequestHandlers(ContainerBuilder builder)
        {
            // Mediatr infrastructure
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(
                context =>
                {
                    var c = context.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });

            // Request handlers
            builder.RegisterAssemblyTypes(ServicesAssembly).AsClosedTypesOf(typeof(IRequestHandler<>)).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(ServicesAssembly).AsClosedTypesOf(typeof(IRequestHandler<,>)).AsImplementedInterfaces();

            // Decorators
            builder.RegisterGenericDecorator(typeof(ConcordanceSearchDecorator<,>), typeof(IRequestHandler<,>));
            builder.RegisterGenericDecorator(typeof(AuditLogDecorator<,>), typeof(IRequestHandler<,>));
        }

        private void RegisterSearch(ContainerBuilder builder)
        {
            // Query factories
            builder.RegisterAssemblyTypes(ServicesAssembly).AsClosedTypesOf(typeof(IQueryFactory<,>)).AsImplementedInterfaces().InstancePerLifetimeScope();

            // Aggregations
            builder.RegisterType<AggregationProviderFactory>().As<IAggregationProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DiscourseChannelAggregationProvider>()
                .Keyed<IAggregationProvider>(AggregationType.DiscourseChannel)
                .InstancePerLifetimeScope();
            builder.RegisterType<DiscourseEventAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.DiscourseEvent).InstancePerLifetimeScope();
            builder.RegisterType<DiscourseRegionAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.DiscourseRegion).InstancePerLifetimeScope();
            builder.RegisterType<DiscourseTypeAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.DiscourseType).InstancePerLifetimeScope();
            builder.RegisterType<DiscourseYearAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.DiscourseYear).InstancePerLifetimeScope();
            builder.RegisterType<SpeakerAgeAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.SpeakerAge).InstancePerLifetimeScope();
            builder.RegisterType<SpeakerEducationAggregationProvider>()
                .Keyed<IAggregationProvider>(AggregationType.SpeakerEducation)
                .InstancePerLifetimeScope();
            builder.RegisterType<SpeakerLanguageAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.SpeakerLanguage).InstancePerLifetimeScope();
            builder.RegisterType<SpeakerRegionAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.SpeakerRegion).InstancePerLifetimeScope();
            builder.RegisterType<SpeakerSexAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.SpeakerSex).InstancePerLifetimeScope();
            builder.RegisterType<PartOfSpeechAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.PartOfSpeech).InstancePerLifetimeScope();
            builder.RegisterType<LemmaAggregationProvider>().Keyed<IAggregationProvider>(AggregationType.Lemma).InstancePerLifetimeScope();

            // Alternate searches
            builder.RegisterType<AlternateSearchProviderFactory>().As<IAlternateSearchProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ConcordanceLemmasAlternateSearchProvider>()
                .Keyed<IAlternateSearchProvider<ConcordanceSearch, ConcordanceSearchResponse>>(AlternateSearchType.Lemmas)
                .InstancePerLifetimeScope();
        }
    }
}