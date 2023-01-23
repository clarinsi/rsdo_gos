using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Framework;
using Gos.Services.Services.PartOfSpeechService;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Services.StatementService
{
    public class StatementService : IStatementService
    {
        private readonly GosDbContext dbContext;
        private readonly IPartOfSpeechService partOfSpeechService;

        public StatementService(GosDbContext dbContext, IPartOfSpeechService partOfSpeechService)
        {
            this.dbContext = dbContext;
            this.partOfSpeechService = partOfSpeechService;
        }

        public async Task<List<string>> GetSoundFiles(Expression<Func<Token, bool>> predicate)
        {
            return await dbContext.Tokens.Include(t => t.Segment).Where(predicate).Select(t => t.Segment.SoundFile).Distinct().ToListAsync();
        }

        public async Task<Statement> GetStatement(Expression<Func<Statement, bool>> predicate)
        {
            var statements = await GetStatements(predicate);
            return statements.FirstOrDefault();
        }

        public async Task<List<Statement>> GetStatements(Expression<Func<Statement, bool>> predicate)
        {
            return await dbContext.Statements.Include(s => s.Discourse)
                .Include(s => s.Discourse.Channel)
                .Include(s => s.Discourse.Channel.Translations)
                .Include(s => s.Discourse.Event)
                .Include(s => s.Discourse.Event.Translations)
                .Include(s => s.Discourse.Region)
                .Include(s => s.Discourse.Region.Translations)
                .Include(s => s.Discourse.Type)
                .Include(s => s.Discourse.Type.Translations)
                .Include(s => s.Speaker.Age)
                .Include(s => s.Speaker.Age.Translations)
                .Include(s => s.Speaker.Education)
                .Include(s => s.Speaker.Education.Translations)
                .Include(s => s.Speaker.Language)
                .Include(s => s.Speaker.Language.Translations)
                .Include(s => s.Speaker.Region1)
                .Include(s => s.Speaker.Region1.Translations)
                .Include(s => s.Speaker.Sex)
                .Include(s => s.Speaker.Sex.Translations)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<ConcordanceToken>> GetTokens(Expression<Func<Token, bool>> predicate)
        {
            // Get tokens from database
            var tokens = await dbContext.Tokens.Include(t => t.Segment)
                .ThenInclude(t => t.Statement)
                .Where(predicate)
                .OrderBy(t => t.DiscourseOrder)
                .ToListAsync();

            // Convert tokens
            var result = new List<ConcordanceToken>();
            foreach (var token in tokens)
            {
                result.Add(
                    new ConcordanceToken()
                    {
                        ConversationalForm = token.ConversationalForm,
                        StandardForm = token.StandardForm,
                        LeftMark = token.LeftMark,
                        Lemma = token.Lemma,
                        Msd = token.Msd,
                        MsdDescription = await partOfSpeechService.GetMsdDescriptionByCode(token.Msd),
                        RightMark = token.RightMark,
                        SegmentId = token.Segment.Id,
                        StatementId = token.Segment.Statement.Id,
                        StatementOrder = token.Segment.Statement.Order,
                        TokenOrder = token.DiscourseOrder,
                    });
            }

            return result;
        }
    }
}