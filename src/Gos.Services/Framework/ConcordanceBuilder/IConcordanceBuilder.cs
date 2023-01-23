using System.Collections.Generic;
using Gos.Core.Entities;
using Gos.Core.Model;

namespace Gos.Services.Framework.ConcordanceBuilder
{
    public interface IConcordanceBuilder
    {
        List<Concordance> GetConcordances(Statement statement, List<Token> tokens);
    }
}