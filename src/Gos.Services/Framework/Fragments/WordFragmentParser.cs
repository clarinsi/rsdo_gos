using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;

namespace Gos.Services.Framework.Fragments
{
    public class WordFragmentParser : IFragmentParser
    {
        public IEnumerable<Token> GetTokens(XElement element)
        {
            var result = new List<Token>();
            var subWords = element.Elements(Constants.TeiNs + "w").ToList();
            if (subWords.Count > 0)
            {
                for (int i = 0; i < subWords.Count; i++)
                {
                    var token = new Token(TokenType.Word);
                    if (element.Attribute("norm") != null)
                    {
                        // Standard form has one word, conversational form has multiple words
                        token.WithConversationForm(GetConversationalForm(subWords[i]));
                        if (i == 0)
                        {
                            token.WithStandardForm(GetStandardForm(element)).WithLemma(GetLemma(element)).WithMsd(GetMsd(element));
                        }
                    }
                    else
                    {
                        // Standard form has multiple word, conversational form has one word
                        token.WithStandardForm(GetStandardForm(subWords[i])).WithLemma(GetLemma(subWords[i])).WithMsd(GetMsd(subWords[i]));
                        if (i == 0)
                        {
                            token.WithConversationForm(GetConversationalForm(element));
                        }
                    }

                    result.Add(token);
                }
            }
            else
            {
                var token = new Token(TokenType.Word).WithConversationForm(GetConversationalForm(element))
                    .WithStandardForm(GetStandardForm(element))
                    .WithLemma(GetLemma(element))
                    .WithMsd(GetMsd(element));
                result.Add(token);
            }

            return result;
        }

        protected virtual string GetConversationalForm(XElement wordEl)
        {
            return wordEl.Value;
        }

        protected virtual string GetStandardForm(XElement wordEl)
        {
            return wordEl.Attribute("norm")?.Value ?? wordEl.Value;
        }

        protected virtual string GetLemma(XElement wordEl)
        {
            return wordEl.Attribute("lemma")?.Value;
        }

        protected virtual string GetMsd(XElement wordEl)
        {
            return wordEl.Attribute("ana")?.Value;
        }
    }
}