using System.Xml.Linq;

namespace Gos.Services.Framework.Fragments
{
    public interface IFragmentParserFactory
    {
        IFragmentParser GetParser(FragmentType fragmentType);

        IFragmentParser GetParser(XName elementName);
    }
}