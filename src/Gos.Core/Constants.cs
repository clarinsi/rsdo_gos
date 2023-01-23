using System.Xml.Linq;

namespace Gos.Core
{
    public static class Constants
    {
        public const string TeiNamespace = "http://www.tei-c.org/ns/1.0";
        public const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";
        public const string IncludeNamespace = "http://www.w3.org/2001/XInclude";
        public static readonly XNamespace TeiNs;
        public static readonly XNamespace XmlNs;
        public static readonly XNamespace IncludeNs;

        static Constants()
        {
            TeiNs = XNamespace.Get(TeiNamespace);
            XmlNs = XNamespace.Get(XmlNamespace);
            IncludeNs = XNamespace.Get(IncludeNamespace);
        }

        public static class InterfaceLanguages
        {
            public static string English = "en";
            public static string EnglishName = "English";
            public static string Slovene = "sl";
            public static string SloveneName = "Slovenščina";
        }

        public static class Formats
        {
            public static string CountsFormat = "#,##0";
        }

        public static class Search
        {
            public static int DefaultPageSize = 20;
        }
    }
}