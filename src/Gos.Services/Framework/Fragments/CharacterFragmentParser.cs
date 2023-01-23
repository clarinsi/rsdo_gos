using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class CharacterFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            var character = element.Value;

            yield return new Token(TokenType.Character).WithConversationForm(character)
                .WithStandardForm(character)
                .WithLemma(character)
                .WithMsd(element.Attribute("ana").Value);
        }
    }
}