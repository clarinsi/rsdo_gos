using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class UnclearWordFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            yield return new Token(TokenType.Word).WithConversationForm("[nerazumljivo]")
                .WithStandardForm("[nerazumljivo]")
                .WithLemma("[nerazumljivo]")
                .WithMsd("mte:Cc");
        }
    }
}