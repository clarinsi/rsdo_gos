using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public abstract class BaseContainerParser : IFragmentParser
    {
        protected readonly IFragmentParserFactory ParserFactory;

        protected BaseContainerParser(IFragmentParserFactory parserFactory)
        {
            ParserFactory = parserFactory;
        }

        public virtual IEnumerable<Token> GetTokens(XElement element)
        {
            var result = new List<Token>();

            foreach (var el in element.Elements())
            {
                if (!IsChildAllowed(el.Name))
                {
                    throw new Exception($"Child {el.Name} is not allowed under {element.Name}!");
                }

                var parser = GetParser(el.Name);
                var tokens = parser.GetTokens(el);
                result.AddRange(tokens);
            }

            return result;
        }

        protected virtual IFragmentParser GetParser(XName name)
        {
            return ParserFactory.GetParser(name);
        }

        protected virtual bool IsChildAllowed(XName name)
        {
            return true;
        }
    }
}