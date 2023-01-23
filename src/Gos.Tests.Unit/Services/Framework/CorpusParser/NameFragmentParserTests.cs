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
    public class NameFragmentParserTests
    {
        private readonly CorpusParserFixture corpusParserFixture;

        public NameFragmentParserTests(CorpusParserFixture corpusParserFixture)
        {
            this.corpusParserFixture = corpusParserFixture;
        }

        [Fact]
        public void Name()
        {
            // Arrange
            var xml = @"<name xmlns=""http://www.tei-c.org/ns/1.0"">
                  <w norm=""HTTPS""
            lemma=""HTTPS""
            ana=""mte:Npmsn""
            msd=""UPosTag=PROPN|Case=Nom|Gender=Masc|Number=Sing""
            xml:id=""GosVL07_kungf.tok5881"">https</w>
                </name>";
            var nameEl = XElement.Parse(xml);
            var parser = new NameFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(nameEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("https").WithStandardForm("HTTPS").WithLemma("HTTPS").WithMsd("mte:Npmsn"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void AnonymizedName()
        {
            // Arrange
            var xml = @"<name xmlns=""http://www.tei-c.org/ns/1.0"" type=""anon"">
                <gap reason=""anon"" quantity=""1"" unit=""words"">
                <desc>[ime]</desc>
                </gap>
                <gap reason=""anon"" quantity=""1"" unit=""words"">
                <desc>[priimek]</desc>
                </gap>
                </name>";
            var nameEl = XElement.Parse(xml);
            var parser = new NameFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(nameEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("[ime]").WithStandardForm("[ime]").WithLemma("[ime]").WithMsd("mte:Xp"),
                new Token(TokenType.Word).WithConversationForm("[priimek]").WithStandardForm("[priimek]").WithLemma("[priimek]").WithMsd("mte:Xp"),
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }

        [Fact]
        public void AnonymizedNameEmpty()
        {
            var xml = @"<name xmlns=""http://www.tei-c.org/ns/1.0"" type=""anon"">
                <gap reason=""anon"" />
                </name>";

            var nameEl = XElement.Parse(xml);
            var parser = new NameFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(nameEl);

            // Assert
            var expected = new List<Token>();
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}