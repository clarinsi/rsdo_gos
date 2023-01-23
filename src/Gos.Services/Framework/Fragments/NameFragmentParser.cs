using System.Xml.Linq;
using Gos.Core;

namespace Gos.Services.Framework.Fragments
{
    public class NameFragmentParser : BaseContainerParser
    {
        public NameFragmentParser(IFragmentParserFactory parserFactory)
            : base(parserFactory)
        {
        }

        protected override bool IsChildAllowed(XName name)
        {
            return name == Constants.TeiNs + "gap" || name == Constants.TeiNs + "w";
        }
    }
}