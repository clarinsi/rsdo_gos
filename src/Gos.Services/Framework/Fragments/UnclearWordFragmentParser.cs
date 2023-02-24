using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class UnclearWordFragmentParser : BaseContainerParser
    {
        public UnclearWordFragmentParser(IFragmentParserFactory parserFactory)
            : base(parserFactory)
        {
        }

        public override IEnumerable<Token> GetTokens(XElement element)
        {
            var tokens = new List<Token>();
            tokens.Add(new Token(TokenType.Mark).WithConversationForm("("));
            tokens.AddRange(base.GetTokens(element));
            tokens.Add(new Token(TokenType.Mark).WithConversationForm(")"));
            return tokens;
        }
    }
}