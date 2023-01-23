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
    public class ForeignFragmentParserTests
    {
        private readonly CorpusParserFixture corpusParserFixture;

        public ForeignFragmentParserTests(CorpusParserFixture corpusParserFixture)
        {
            this.corpusParserFixture = corpusParserFixture;
        }

        [Fact]
        public void Foreign()
        {
            // Arrange
            var xml = @"<foreign xmlns=""http://www.tei-c.org/ns/1.0"">
                <w xml:id=""gos020.tok3452""
            norm=""arbeit""
            lemma=""arbeit""
            ana=""mte:Xf"">arbajt</w>
                <w xml:id=""gos020.tok3453"" norm=""macht"" lemma=""macht"" ana=""mte:Xf"">maht</w>
                <w xml:id=""gos020.tok3454"" norm=""frei"" lemma=""frei"" ana=""mte:Xf"">fraj</w>
                </foreign>";
            var foreignEl = XElement.Parse(xml);
            var parser = new ForeignFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(foreignEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("arbajt").WithStandardForm("arbeit").WithLemma("arbeit").WithMsd("mte:Xf"),
                new Token(TokenType.Word).WithConversationForm("maht").WithStandardForm("macht").WithLemma("macht").WithMsd("mte:Xf"),
                new Token(TokenType.Word).WithConversationForm("fraj").WithStandardForm("frei").WithLemma("frei").WithMsd("mte:Xf"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}