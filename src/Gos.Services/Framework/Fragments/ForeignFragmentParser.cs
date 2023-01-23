using System;
using System.Linq;

namespace Gos.Services.Framework.Fragments
{
    public class ForeignFragmentParser : BaseContainerParser
    {
        public ForeignFragmentParser(IFragmentParserFactory parserFactory)
            : base(parserFactory)
        {
        }
    }
}