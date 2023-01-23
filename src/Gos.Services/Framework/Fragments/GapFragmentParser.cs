using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class GapFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            if (element.Attribute("reason").Value == "unclear")
            {
                return GetUnclear();
            }

            if (element.Attribute("reason").Value == "anon")
            {
                return GetAnon(element);
            }

            throw new Exception($"Unknown gap reason {element.Attribute("reason").Value}!");
        }

        private IEnumerable<Token> GetAnon(XElement element)
        {
            var tokens = new List<Token>();

            var descEl = element.Element(Constants.TeiNs + "desc");
            if (descEl != null)
            {
                var desc = descEl.Value;
                tokens.Add(new Token(TokenType.Word).WithConversationForm(desc).WithStandardForm(desc).WithLemma(desc).WithMsd("mte:Xp"));
            }

            return tokens;
        }

        private IEnumerable<Token> GetUnclear()
        {
            yield return new Token(TokenType.Word).WithConversationForm("[nerazumljivo]")
                .WithStandardForm("[nerazumljivo]")
                .WithLemma("[nerazumljivo]")
                .WithMsd("mte:Cc");
        }
    }
}