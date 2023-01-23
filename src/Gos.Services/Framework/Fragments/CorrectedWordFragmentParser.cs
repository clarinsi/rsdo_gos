using System.Xml.Linq;

namespace Gos.Services.Framework.Fragments
{
    public class CorrectedWordFragmentParser : WordFragmentParser
    {
        protected override string GetConversationalForm(XElement wordEl)
        {
            var conversational = base.GetConversationalForm(wordEl);
            return $"{conversational}()";
        }

        protected override string GetStandardForm(XElement wordEl)
        {
            var standard = base.GetStandardForm(wordEl);
            return $"{standard}()";
        }

        protected override string GetLemma(XElement wordEl)
        {
            var lemma = base.GetLemma(wordEl);
            return $"{lemma}()";
        }
    }
}