using System.Collections.Generic;
using System.Xml.Linq;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;
using Gos.Services.Framework.Fragments;
using Gos.Tests.Unit.Fixtures;
using Gos.Tests.Unit.Services.Framework.Comparers;
using Xunit;

namespace Gos.Tests.Unit.Services.Framework.CorpusParser
{
    [Collection(nameof(CorpusParserCollection))]
    public class CorrectionFragmentParserTests
    {
        private readonly CorpusParserFixture corpusParserFixture;

        public CorrectionFragmentParserTests(CorpusParserFixture corpusParserFixture)
        {
            this.corpusParserFixture = corpusParserFixture;
        }

        [Fact]
        public void Correction()
        {
            // Arrange
            var xml =
                $@"<del xmlns=""http://www.tei-c.org/ns/1.0"" type=""falseStart""><w lemma=""p"" ana=""mte:Xt"" xml:id=""GosVL01_pravo.tok318"">p</w></del>";
            var correctionEl = XElement.Parse(xml);
            var parser = new CorrectionFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(correctionEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("p()").WithStandardForm("p()").WithLemma("p()").WithMsd("mte:Xt")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}