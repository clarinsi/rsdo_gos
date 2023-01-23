using System.Xml.Linq;
using Gos.Core;

namespace Gos.Services.Framework.Fragments
{
    public class CorrectionFragmentParser : BaseContainerParser
    {
        public CorrectionFragmentParser(IFragmentParserFactory parserFactory)
            : base(parserFactory)
        {
        }

        protected override bool IsChildAllowed(XName name)
        {
            return name == Constants.TeiNs + "w";
        }

        protected override IFragmentParser GetParser(XName name)
        {
            if (name == Constants.TeiNs + "w")
            {
                return ParserFactory.GetParser(FragmentType.CorrectedWord);
            }

            return base.GetParser(name);
        }
    }
}