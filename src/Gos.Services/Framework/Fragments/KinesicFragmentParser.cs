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
            yield return new Token(TokenType.Word).WithConversationForm("[aplavz]").WithStandardForm("[aplavz]").WithLemma("[aplavz]").WithMsd("mte:Xt");
        }
    }
}