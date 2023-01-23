using System.Collections.Generic;
using System.IO;
using Gos.Core;
using Gos.Core.Entities;

namespace Gos.Services.Framework.SeedData
{
    public static class MsdSeedData
    {
        public static IEnumerable<Msd> Get()
        {
            var assembly = typeof(PartOfSpeechSeedData).Assembly;
            var resourceName = $"Gos.Services.Framework.SeedData.CsvFiles.msd.csv";
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var streamReader = new StreamReader(stream);

            // Skip header line
            streamReader.ReadLine();

            // Loop through other lines
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                yield return ReadFromLine(line);
            }
        }

        public static Msd ReadFromLine(string line)
        {
            var values = line.Split(";");
            return new Msd()
            {
                Code = values[0],
                Translations = new TranslationCollection<MsdTranslation>()
                {
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.Slovene,
                        Title = values[3],
                    },
                    new()
                    {
                        CultureName = Constants.InterfaceLanguages.English,
                        Title = values[1],
                    },
                },
            };
        }
    }
}