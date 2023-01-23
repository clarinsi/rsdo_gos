using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.ConcordanceBuilder
{
    public class ConcordanceBuilder : IConcordanceBuilder
    {
        public List<Concordance> GetConcordances(Statement statement, List<Token> tokens)
        {
            var concordances = new List<Concordance>();
            for (var i = 0; i < tokens.Count; i++)
            {
                var concordance = new Concordance()
                {
                    Statement = statement,
                };

                // set center token
                concordance.SetToken(tokens[i], 0);

                // set left and right context tokens
                for (var c = 1; c <= 10; c++)
                {
                    // left context
                    if (i - c >= 0)
                    {
                        concordance.SetToken(tokens[i - c], -c);
                    }

                    // right context
                    if (i + c < tokens.Count)
                    {
                        concordance.SetToken(tokens[i + c], c);
                    }
                }

                concordances.Add(concordance);
            }

            return concordances;
        }
    }
}