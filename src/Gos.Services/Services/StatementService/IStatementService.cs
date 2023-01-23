using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Services.Services.StatementService
{
    public interface IStatementService
    {
        Task<List<string>> GetSoundFiles(Expression<Func<Token, bool>> predicate);

        Task<Statement> GetStatement(Expression<Func<Statement, bool>> predicate);

        Task<List<Statement>> GetStatements(Expression<Func<Statement, bool>> predicate);

        Task<List<ConcordanceToken>> GetTokens(Expression<Func<Token, bool>> predicate);
    }
}