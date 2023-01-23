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
    public class SegmentFragmentParserTests
    {
        private readonly CorpusParserFixture corpusParserFixture;

        public SegmentFragmentParserTests(CorpusParserFixture corpusParserFixture)
        {
            this.corpusParserFixture = corpusParserFixture;
        }

        [Fact]
        public void Segment()
        {
            // Arrange
            var xml = @"<seg xmlns=""http://www.tei-c.org/ns/1.0"" xml:id=""GosVL02_kleme.s4"">
                <w lemma=""in""
            ana=""mte:Cc""
            msd=""UPosTag=CCONJ""
            xml:id=""GosVL02_kleme.tok92"">in</w>
                </seg>";
            var segmentEl = XElement.Parse(xml);
            var parser = new SegmentFragmentParser(corpusParserFixture.FragmentParserFactory);

            // Act
            var tokens = parser.GetTokens(segmentEl);

            // Assert
            var expected = new List<Token>()
            {
                new Token(TokenType.Word).WithConversationForm("in").WithStandardForm("in").WithLemma("in").WithMsd("mte:Cc")
            };
            Assert.Equal(expected, tokens, new FragmentParserTokenComparer());
        }
    }
}