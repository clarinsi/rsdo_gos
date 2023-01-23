using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.List;
using Gos.ServiceModel.Requests.Shared;
using Gos.Services.Resources;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.PartOfSpeechService;
using MediatR;

namespace Gos.Services.RequestHandlers.List
{
    public class ListExportHandler : BaseListHandler, IRequestHandler<ListExport, ExportResponse>
    {
        private readonly IQueryFactory<ListExport, ListQuery> queryFactory;
        private readonly ISearchEngine searchEngine;

        public ListExportHandler(IPartOfSpeechService partOfSpeechService, IQueryFactory<ListExport, ListQuery> queryFactory, ISearchEngine searchEngine)
            : base(partOfSpeechService)
        {
            this.queryFactory = queryFactory;
            this.searchEngine = searchEngine;
        }

        public async Task<ExportResponse> Handle(ListExport request, CancellationToken cancellationToken)
        {
            // Get query
            var query = await queryFactory.GetQuery(request);

            // Get results
            var results = searchEngine.Search<ListQuery, ListQueryResult>(query);

            // Sort and page results
            var items = (await GetResponseItems(results.Items)).AsQueryable()
                .OrderBy(request.SortField, request.SortDirection == SortDirection.Descending)
                .Take(request.Rows)
                .ToList();

            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.Default, 1024, true))
            {
                // Write header
                await writer.WriteAsync(ExportResource.ConversationalForm + "\t");
                await writer.WriteAsync(ExportResource.Frequency + "\t");
                await writer.WriteAsync(ExportResource.StandardizedForm);
                if (request.GroupByMsd)
                {
                    await writer.WriteAsync("\t" + ExportResource.Msd);
                }

                await writer.WriteLineAsync();

                // Write items
                foreach (var item in items)
                {
                    await writer.WriteAsync(item.ConversationalForm + "\t");
                    await writer.WriteAsync(item.Frequency + "\t");
                    await writer.WriteAsync(item.StandardForm);
                    if (request.GroupByMsd)
                    {
                        await writer.WriteAsync("\t" + item.MsdDescription);
                    }

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
    }
}