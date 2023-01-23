using System;
using System.Xml.Linq;
using Autofac.Features.Indexed;
using Gos.Core;

namespace Gos.Services.Framework.Fragments
{
    public class FragmentParserFactory : IFragmentParserFactory
    {
        private readonly IIndex<FragmentType, IFragmentParser> parsers;

        public FragmentParserFactory(IIndex<FragmentType, IFragmentParser> parsers)
        {
            this.parsers = parsers;
        }

        public IFragmentParser GetParser(FragmentType fragmentType) => parsers[fragmentType];

        public IFragmentParser GetParser(XName elementName)
        {
            if (elementName == Constants.TeiNs + "w")
            {
                return parsers[FragmentType.Word];
            }

            if (elementName == Constants.TeiNs + "pc")
            {
                return parsers[FragmentType.Character];
            }

            if (elementName == Constants.TeiNs + "name")
            {
                return parsers[FragmentType.Name];
            }

            if (elementName == Constants.TeiNs + "pause")
            {
                return parsers[FragmentType.Pause];
            }

            if (elementName == Constants.TeiNs + "vocal")
            {
                return parsers[FragmentType.Vocal];
            }

            if (elementName == Constants.TeiNs + "incident")
            {
                return parsers[FragmentType.Incident];
            }

            if (elementName == Constants.TeiNs + "gap")
            {
                return parsers[FragmentType.Gap];
            }

            if (elementName == Constants.TeiNs + "del")
            {
                return parsers[FragmentType.Correction];
            }

            if (elementName == Constants.TeiNs + "foreign")
            {
                return parsers[FragmentType.Foreign];
            }

            throw new Exception($"Parser for element {elementName} not found!");
        }
    }
}