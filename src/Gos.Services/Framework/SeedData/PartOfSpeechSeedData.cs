using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class PartOfSpeechSeedData
    {
        public static IEnumerable<PartOfSpeech> Get()
        {
            var names = new string[]
            {
                "Noun",
                "Verb",
                "Adjective",
                "Adverb",
                "Pronoun",
                "Numeral",
                "Adposition",
                "Conjunction",
                "Particle",
                "Interjection",
                "Abbreviation",
                "Residual",
                "Punctuation",
            };
            for (int i = 0; i < names.Length; i++)
            {
                var assembly = typeof(PartOfSpeechSeedData).Assembly;
                var resourceName = $"Gos.Services.Framework.SeedData.CsvFiles.{names[i].ToLower()}.csv";
                using var stream = assembly.GetManifestResourceStream(resourceName);
                yield return ReadFromStream(stream, (short)(i + 1));
            }
        }

        private static PartOfSpeech ReadFromStream(Stream stream, short recordOrder)
        {
            using var streamReader = new StreamReader(stream);

            // Skip header line
            streamReader.ReadLine();

            // Loop through other lines
            string line;
            short attributeOrder = 0;
            short attributeValueOrder = 0;
            PartOfSpeech partOfSpeech = null;
            PartOfSpeechAttribute partOfSpeechAttribute = null;
            while ((line = streamReader.ReadLine()) != null)
            {
                var values = line.Split(";");
                if (values[0] == "0")
                {
                    partOfSpeech = GetPartOfSpeech(values, recordOrder);
                }
                else
                {
                    if (values[0] != "")
                    {
                        attributeOrder++;
                        partOfSpeechAttribute = GetPartOfSpeechAttribute(values, attributeOrder);
                        partOfSpeech.Attributes.Add(partOfSpeechAttribute);
                        attributeValueOrder = 0;
                    }

                    attributeValueOrder++;
                    var partOfSpeechAttributeValue = GetPartOfSpeechAttributeValue(values, attributeValueOrder);
                    partOfSpeechAttribute.Values.Add(partOfSpeechAttributeValue);
                }
            }

            return partOfSpeech;
        }

        private static PartOfSpeech GetPartOfSpeech(string[] values, short recordOrder)
        {
            return new PartOfSpeech()
            {
                Code = values[6],
                RecordOrder = recordOrder,
                Translations = new TranslationCollection<PartOfSpeechTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = values[2],
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = values[5],
                    },
                },
            };
        }

        private static PartOfSpeechAttribute GetPartOfSpeechAttribute(string[] values, short recordOrder)
        {
            return new PartOfSpeechAttribute()
            {
                RecordOrder = recordOrder,
                Translations = new TranslationCollection<PartOfSpeechAttributeTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = values[1],
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = values[4],
                    },
                },
            };
        }

        private static PartOfSpeechAttributeValue GetPartOfSpeechAttributeValue(string[] values, short recordOrder)
        {
            return new PartOfSpeechAttributeValue()
            {
                Code = values[6],
                RecordOrder = recordOrder,
                Translations = new TranslationCollection<PartOfSpeechAttributeValueTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = values[2],
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = values[5],
                    },
                },
            };
        }
    }
}