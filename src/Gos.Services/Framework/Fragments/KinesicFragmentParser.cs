using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class KinesicFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            if (element.Attribute("type")?.Value == "applause")
            {
                yield return new Token(TokenType.Word).WithConversationForm("[aplavz]").WithStandardForm("[aplavz]").WithLemma("[aplavz]").WithMsd("mte:Xt");
            }
            else
            {
                throw new Exception($"Unknown kinesic type {element.Attribute("type")?.Value}!");
            }
        }
    }
}