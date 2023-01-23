using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class IncidentFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            if (element.Attribute("type")?.Value == "sound")
            {
                yield return new Token(TokenType.Word).WithConversationForm("[zvok]").WithStandardForm("[zvok]").WithLemma("[zvok]").WithMsd("mte:Xt");
            }
            else
            {
                throw new Exception($"Unknown incident type {element.Attribute("type")?.Value}!");
            }
        }
    }
}