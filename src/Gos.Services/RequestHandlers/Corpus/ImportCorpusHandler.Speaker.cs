using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public partial class ImportCorpusHandler
    {
        private async Task<SpeakerAge> GetSpeakerAge(XElement ageEl)
        {
            var age = ageEl?.Value;
            var atLeast = ageEl?.Attribute("atLeast")?.Value;
            var atMost = ageEl?.Attribute("atMost")?.Value;
            var isRange = !string.IsNullOrEmpty(atLeast) || !string.IsNullOrEmpty(atMost);

            if (string.IsNullOrEmpty(age) && !isRange || !string.IsNullOrEmpty(age) && age == "nedoločeno")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Neznano);
            }

            if (atMost == "10")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Do10Let);
            }

            if (atLeast == "10" && atMost == "18")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Od10Do18Let);
            }

            if (atLeast == "18" && atMost == "34")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Od18Do34Let);
            }

            if (atLeast == "30" && atMost == "59")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Od30Do59Let);
            }

            if (atLeast == "60")
            {
                return await dbContext.SpeakerAges.SingleAsync(x => x.Id == (int)SpeakerAgeKeys.Nad60Let);
            }

            throw new Exception($"Invalid speaker age {age}; atLeast {atLeast}, atMost {atMost}!");
        }

        private async Task<SpeakerEducation> GetSpeakerEducation(XElement educationEl)
        {
            var education = educationEl?.Value;

            if (string.IsNullOrEmpty(education) || education == "nedoločno")
            {
                return await dbContext.SpeakerEducations.SingleAsync(x => x.Id == (int)SpeakerEducationKeys.Neznano);
            }

            switch (education)
            {
                case "OŠ ali manj":
                    return await dbContext.SpeakerEducations.SingleAsync(x => x.Id == (int)SpeakerEducationKeys.OsAliManj);
                case "srednja šola":
                    return await dbContext.SpeakerEducations.SingleAsync(x => x.Id == (int)SpeakerEducationKeys.SrednjaSola);
                case "višja ali visoka šola":
                    return await dbContext.SpeakerEducations.SingleAsync(x => x.Id == (int)SpeakerEducationKeys.VisjaAliVisokaSola);
                case "fakulteta ali več":
                    return await dbContext.SpeakerEducations.SingleAsync(x => x.Id == (int)SpeakerEducationKeys.FakultetaAliVec);
                default:
                    throw new Exception($"Invalid speaker education {education}!");
            }
        }

        private async Task<SpeakerLanguage> GetSpeakerLanguage(XElement languageEl)
        {
            var language = languageEl?.Attribute("tag")?.Value;

            if (string.IsNullOrEmpty(language))
            {
                return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Neznano);
            }

            switch (language)
            {
                case "sl":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Slovenscina);
                case "de":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Nemscina);
                case "en":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Anglescina);
                case "fr":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Francoscina);
                case "hu":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Madzarscina);
                case "hr":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Hrvascina);
                case "sr":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Srbscina);
                case "it":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Italijanscina);
                case "ru":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Ruscina);
                case "zls":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.Juznoslovanski);
                case "sla":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.DrugiSlovanski);
                case "roa":
                    return await dbContext.SpeakerLanguages.SingleAsync(x => x.Id == (int)SpeakerLanguageKeys.DrugiRomanski);
                default:
                    throw new Exception($"Invalid speaker language {language}!");
            }
        }

        private async Task<SpeakerRegion> GetSpeakerRegion(XElement residenceEl, int i)
        {
            var region = residenceEl?.Value;
            if (string.IsNullOrEmpty(region))
            {
                return null;
            }

            var regions = region.Split(',');
            if (i >= regions.Length)
            {
                return null;
            }

            region = regions[i].Trim();

            switch (region)
            {
                case "savinjska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Savinjska);
                case "goriška":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Goriska);
                case "posavska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Posavska);
                case "obalno-kraška":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.ObalnoKraska);
                case "gorenjska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Gorenjska);
                case "osrednjeslovenska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Osrednjeslovenska);
                case "podravska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Podravska);
                case "pomurska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Pomurska);
                case "jugovzhodna Slovenija":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.JugovzhodnaSlovenija);
                case "koroška":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Koroška);
                case "zasavska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Zasavska);
                case "primorsko-notranjska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.PrimorskoNotranjska);
                case "Avstrija":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Avstrija);
                case "Italija":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Italija);
                case "Madžarska":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Madzarska);
                case "tujina":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Tujina);
                case "nedoločno":
                    return await dbContext.SpeakerRegions.SingleAsync(x => x.Id == (int)SpeakerRegionKeys.Neznano);
                default:
                    throw new Exception($"Invalid speaker region {region}!");
            }
        }

        private async Task<SpeakerSex> GetSpeakerSex(XElement personEl)
        {
            var sex = personEl.Attribute("sex")?.Value;

            if (string.IsNullOrEmpty(sex) || sex == "-")
            {
                return await dbContext.SpeakerSexes.SingleAsync(x => x.Id == (int)SpeakerSexKeys.Neznano);
            }

            switch (sex)
            {
                case "M":
                    return await dbContext.SpeakerSexes.SingleAsync(x => x.Id == (int)SpeakerSexKeys.Moski);
                case "F":
                    return await dbContext.SpeakerSexes.SingleAsync(x => x.Id == (int)SpeakerSexKeys.Zenski);
                default:
                    throw new Exception($"Invalid speaker sex {sex}!");
            }
        }

        private async Task ImportSpeaker(XElement personEl)
        {
            var code = personEl.Attribute(Constants.XmlNs + "id").Value;

            var speaker = new Speaker
            {
                Code = code
            };

            // Sex
            speaker.Sex = await GetSpeakerSex(personEl);

            // Age
            var ageEl = personEl.Element(Constants.TeiNs + "age");
            speaker.Age = await GetSpeakerAge(ageEl);

            // Region
            var residenceEl = personEl.Element(Constants.TeiNs + "residence");
            speaker.Region1 = await GetSpeakerRegion(residenceEl, 0);
            speaker.Region2 = await GetSpeakerRegion(residenceEl, 1);
            speaker.Region3 = await GetSpeakerRegion(residenceEl, 2);

            // Education
            var educationEl = personEl.Element(Constants.TeiNs + "education");
            speaker.Education = await GetSpeakerEducation(educationEl);

            // Language
            var languageEl = personEl.Element(Constants.TeiNs + "langKnowledge")?.Element(Constants.TeiNs + "langKnown");
            speaker.Language = await GetSpeakerLanguage(languageEl);

            // Save
            await dbContext.Speakers.AddAsync(speaker);
            await dbContext.SaveChangesAsync();
        }

        private async Task ImportSpeakers(XElement listPersonEl)
        {
            foreach (var personEl in listPersonEl.Elements(Constants.TeiNs + "person"))
            {
                await ImportSpeaker(personEl);
            }
        }
    }
}