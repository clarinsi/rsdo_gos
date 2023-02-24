using System;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Extensions;
using Gos.Services.Search.QueryFactories;
using Gos.Services.Services.StatementService;
using MediatR;

namespace Gos.Services.RequestHandlers.Concordance
{
    public class ConcordanceDetailsHandler : IRequestHandler<ConcordanceDetails, ConcordanceDetailsResponse>
    {
        private readonly IQueryFactory<ConcordanceDetails, ConcordanceQuery> queryFactory;
        private readonly IStatementService statementService;

        public ConcordanceDetailsHandler(IQueryFactory<ConcordanceDetails, ConcordanceQuery> queryFactory, IStatementService statementService)
        {
            this.queryFactory = queryFactory;
            this.statementService = statementService;
        }

        public async Task<ConcordanceDetailsResponse> Handle(ConcordanceDetails request, CancellationToken cancellationToken)
        {
            var statement = await statementService.GetStatement(s => s.Id == request.StatementId);
            var previous = statement.Order > 0
                ? await statementService.GetStatement(s => s.Order == statement.Order - 1 && s.Discourse.Id == statement.Discourse.Id)
                : null;
            var next = await statementService.GetStatement(s => s.Order == statement.Order + 1 && s.Discourse.Id == statement.Discourse.Id);

            var response = new ConcordanceDetailsResponse
            {
                Statement = await GetResponseStatement(statement),
                PreviousStatement = await GetResponseStatement(previous),
                NextStatement = await GetResponseStatement(next),
            };

            // Highlight main statement
            var query = await queryFactory.GetQuery(request);
            response.Statement.Tokens.Highlight(query, statement.Order, request.TokenOrder);

            // Return response
            return response;
        }

        private async Task<ConcordanceDetailsResponseStatement> GetResponseStatement(Statement statement)
        {
            if (statement == null)
            {
                return null;
            }

            return new ConcordanceDetailsResponseStatement
            {
                DiscourseChannel = statement.Discourse.Channel?.Title,
                DiscourseDescription = statement.Discourse.Description,
                DiscourseEvent = statement.Discourse.Event?.Title,
                DiscourseSource = statement.Discourse.Source,
                DiscourseType = statement.Discourse.Type?.Title,
                Id = statement.Id,
                NumberOfSpeakers = statement.Discourse.NumberOfSpeakers,
                SpeakerAge = statement.Speaker?.Age?.Title,
                SpeakerCode = statement.Speaker?.Code,
                SpeakerEducation = statement.Speaker?.Education?.Title,
                SpeakerLanguage = statement.Speaker?.Language?.Title,
                SpeakerRegion = statement.Speaker?.Region1?.Title,
                SpeakerSex = statement.Speaker?.Sex?.Title,
                Tokens = await statementService.GetTokens(t => t.Segment.Statement.Id == statement.Id),
                SoundFiles = await statementService.GetSoundFiles(t => t.Segment.Statement.Id == statement.Id),
            };
        }
    }
}