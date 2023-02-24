using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.ServiceModel.Requests.Shared;
using Gos.Services.Resources;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.StatementService;
using MediatR;

namespace Gos.Services.RequestHandlers.Concordance
{
    public class ConcordanceExportHandler : BaseConcordanceHandler, IRequestHandler<ConcordanceExport, ExportResponse>
    {
        private readonly IQueryFactory<ConcordanceExport, ConcordanceQuery> queryFactory;
        private readonly ISearchEngine searchEngine;

        public ConcordanceExportHandler(
            IQueryFactory<ConcordanceExport, ConcordanceQuery> queryFactory,
            ISearchEngine searchEngine,
            IStatementService statementService)
            : base(statementService)
        {
            this.queryFactory = queryFactory;
            this.searchEngine = searchEngine;
        }

        public async Task<ExportResponse> Handle(ConcordanceExport request, CancellationToken cancellationToken)
        {
            // Get query
            var query = await queryFactory.GetQuery(request);

            // Get results
            var results = searchEngine.Search<ConcordanceQuery, ConcordanceQueryResult>(query);
            var items = await GetResponseItems(query, results.Items);

            // Get center context statements (for details about discourse and speakers)
            var statementIds = items.Select(s => s.CenterContext.StatementId).ToList();
            var statements = (await StatementService.GetStatements(s => statementIds.Contains(s.Id))).ToDictionary(s => s.Id, s => s);

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.Default, 1024, true))
            {
                // Write header
                await writer.WriteAsync(ExportResource.LeftContext + "\t");
                await writer.WriteAsync(request.Query + "\t");
                await writer.WriteAsync(ExportResource.RightContext + "\t");
                await writer.WriteAsync(ExportResource.StandardizedForm + "\t");
                await writer.WriteAsync(ExportResource.DiscourseType + "\t");
                await writer.WriteAsync(ExportResource.DiscourseChannel + "\t");
                await writer.WriteAsync(ExportResource.DiscourseSource + "\t");
                await writer.WriteAsync(ExportResource.DiscourseYear + "\t");
                await writer.WriteAsync(ExportResource.DiscourseEvent + "\t");
                await writer.WriteAsync(ExportResource.SpeakerSex + "\t");
                await writer.WriteAsync(ExportResource.SpeakerAge + "\t");
                await writer.WriteAsync(ExportResource.SpeakerRegion + "\t");
                await writer.WriteAsync(ExportResource.SpeakerEducation + "\t");
                await writer.WriteAsync(ExportResource.SpeakerLanguage);
                await writer.WriteLineAsync();

                // Write items
                foreach (var item in items)
                {
                    var centerStatement = statements[item.CenterContext.StatementId];
                    await writer.WriteAsync(item.LeftContext.ToConversationalFormText() + "\t");
                    await writer.WriteAsync(item.CenterContext.ConversationalForm + "\t");
                    await writer.WriteAsync(item.RightContext.ToConversationalFormText() + "\t");
                    await writer.WriteAsync(GetStandardizedForm(item) + "\t");
                    await writer.WriteAsync(centerStatement.Discourse.Type.Title + "\t");
                    await writer.WriteAsync(centerStatement.Discourse.Channel.Title + "\t");
                    await writer.WriteAsync(centerStatement.Discourse.Source + "\t");
                    await writer.WriteAsync(centerStatement.Discourse.Date.Year + "\t");
                    await writer.WriteAsync(centerStatement.Discourse.Event.Title + "\t");
                    await writer.WriteAsync(centerStatement.Speaker?.Sex?.Title + "\t");
                    await writer.WriteAsync(centerStatement.Speaker?.Age?.Title + "\t");
                    await writer.WriteAsync(centerStatement.Speaker?.Region1?.Title + "\t");
                    await writer.WriteAsync(centerStatement.Speaker?.Education?.Title + "\t");
                    await writer.WriteAsync(centerStatement.Speaker?.Language?.Title);
                    await writer.WriteLineAsync();
                }
            }

            stream.Position = 0;
            return new ExportResponse()
            {
                ContentType = "text/plain",
                FileName = "export.txt",
                Stream = stream,
            };
        }

        private static string GetStandardizedForm(ConcordanceSearchResponseItem item)
        {
            var sb = new StringBuilder();
            Append(item.LeftContext.ToStandardFormText());
            Append(item.CenterContext.StandardForm);
            Append(item.RightContext.ToStandardFormText());
            return sb.ToString();

            void Append(string text)
            {
                if (sb.Length > 0)
                {
                    sb.Append(' ');
                }

                sb.Append(text);
            }
        }
    }
}