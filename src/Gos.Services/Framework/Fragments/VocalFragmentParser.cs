using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class VocalFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            var type = element.Attribute("type")?.Value switch
            {
                "laughter" => "[smeh]",
                "voice" => "[glas]",
                _ => throw new Exception($"Unknown vocal type {element.Attribute("type")?.Value}!"),
            };

            yield return new Token(TokenType.Mark).WithConversationForm(type).WithStandardForm(type).WithLemma(type).WithMsd("mte:Xt");
        }
    }
}