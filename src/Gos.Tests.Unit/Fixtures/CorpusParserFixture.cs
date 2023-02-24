using Autofac.Features.Indexed;
using Gos.Services.Framework.Fragments;
using Moq;
using Xunit;

namespace Gos.Tests.Unit.Fixtures
{
    public class CorpusParserFixture
    {
        public IFragmentParserFactory FragmentParserFactory { get; set; }

        public CorpusParserFixture()
        {
            var indexMock = new Mock<IIndex<FragmentType, IFragmentParser>>();
            FragmentParserFactory = new FragmentParserFactory(indexMock.Object);
            indexMock.Setup(x => x[FragmentType.Word]).Returns(new WordFragmentParser());
            indexMock.Setup(x => x[FragmentType.Character]).Returns(new CharacterFragmentParser());
            indexMock.Setup(x => x[FragmentType.Name]).Returns(new NameFragmentParser(FragmentParserFactory));
            indexMock.Setup(x => x[FragmentType.Vocal]).Returns(new VocalFragmentParser());
            indexMock.Setup(x => x[FragmentType.Correction]).Returns(new CorrectionFragmentParser(FragmentParserFactory));
            indexMock.Setup(x => x[FragmentType.CorrectedWord]).Returns(new CorrectedWordFragmentParser());
            indexMock.Setup(x => x[FragmentType.Unclear]).Returns(new UnclearWordFragmentParser(FragmentParserFactory));
            indexMock.Setup(x => x[FragmentType.Gap]).Returns(new GapFragmentParser());
        }
    }

    [CollectionDefinition(nameof(CorpusParserCollection))]
    public class CorpusParserCollection : ICollectionFixture<CorpusParserFixture>
    {
    }
}