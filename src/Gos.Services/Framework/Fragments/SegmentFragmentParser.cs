using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class SegmentFragmentParser : BaseContainerParser
    {
        public SegmentFragmentParser(IFragmentParserFactory parserFactory)
            : base(parserFactory)
        {
        }

        public override IEnumerable<Token> GetTokens(XElement element)
        {
            try
            {
                return base.GetTokens(element);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}