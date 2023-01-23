using System.Threading.Tasks;
using Autofac;
using CommandLine;
using Gos.Console.CompositionRoot;
using Gos.ServiceModel.Requests.Corpus;
using Gos.Services.Framework;
using MediatR;

namespace Gos.Console
{
    internal class Program
    {
        private static IContainer container;

        private static async Task<int> HandleDeleteCorpusIndex(IMediator mediator, DeleteCorpusIndexOptions opt)
        {
            await mediator.Send(new DeleteCorpusIndex());
            return 3;
        }

        private static async Task<int> HandleImportCorpus(IMediator mediator, ImportCorpusOptions opt)
        {
            await mediator.Send(
                new ImportCorpus
                {
                    SourcePath = opt.SourcePath
                });
            return 1;
        }

        private static async Task<int> HandleIndexCorpus(IMediator mediator, IndexCorpusOptions opt)
        {
            // Warm up cache with part of speeches and msds
            container.Resolve<CacheWarmUp>().WarmUp();
            await mediator.Send(new IndexCorpus());
            return 2;
        }

        private static async Task<int> HandleImportCounters(IMediator mediator, ImportCountersOptions opt)
        {
            await mediator.Send(
                new ImportCounters
                {
                    SourcePath = opt.SourcePath,
                });
            return 3;
        }

        private static async Task<int> HandleImportLexicon(IMediator mediator, ImportLexiconOptions opt)
        {
            await mediator.Send(
                new ImportLexicon()
                {
                    SourcePath = opt.SourcePath,
                });
            return 4;
        }

        public static async Task Main(string[] args)
        {
            container = new ConsoleBootstrapper().Bootstrap();
            var mediator = container.Resolve<IMediator>();

            await Parser.Default
                .ParseArguments<ImportCorpusOptions, IndexCorpusOptions, DeleteCorpusIndexOptions, ImportCountersOptions, ImportLexiconOptions>(args)
                .MapResult(
                    (ImportCorpusOptions opt) => HandleImportCorpus(mediator, opt),
                    (DeleteCorpusIndexOptions opt) => HandleDeleteCorpusIndex(mediator, opt),
                    (IndexCorpusOptions opt) => HandleIndexCorpus(mediator, opt),
                    (ImportCountersOptions opt) => HandleImportCounters(mediator, opt),
                    (ImportLexiconOptions opt) => HandleImportLexicon(mediator, opt),
                    errs => Task.FromResult(0));

            System.Console.WriteLine("Finished...");
        }

        [Verb("deleteindex", HelpText = "Delete corpus index")]
        private class DeleteCorpusIndexOptions
        {
        }

        [Verb("import", HelpText = "Imports corpus to database")]
        private class ImportCorpusOptions
        {
            [Option("path", HelpText = "Full path to xml file")]
            public string SourcePath { get; set; }
        }

        [Verb("index", HelpText = "Indexes corpus to ElasticSearch")]
        private class IndexCorpusOptions
        {
        }

        [Verb("counters", HelpText = "Imports counters and forms/lemmas to database")]
        private class ImportCountersOptions
        {
            [Option("path", HelpText = "Full path to xml file")]
            public string SourcePath { get; set; }
        }

        [Verb("lexicon", HelpText = "Imports lexicon forms/lemmas to database")]
        private class ImportLexiconOptions
        {
            [Option("path", HelpText = "Full path to xml file")]
            public string SourcePath { get; set; }
        }
    }
}