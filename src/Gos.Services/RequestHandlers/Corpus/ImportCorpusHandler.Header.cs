using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public partial class ImportCorpusHandler
    {
        private async Task<DiscourseChannel> GetDiscourseChannel(XElement channelEl)
        {
            var channel = channelEl.Attribute("target").Value;

            switch (channel)
            {
                case "#gos.K.R":
                    return await dbContext.DiscourseChannels.SingleAsync(x => x.Id == (int)DiscourseChannelKeys.Radio);
                case "#gos.K.T":
                    return await dbContext.DiscourseChannels.SingleAsync(x => x.Id == (int)DiscourseChannelKeys.Televizija);
                case "#gos.K.O":
                    return await dbContext.DiscourseChannels.SingleAsync(x => x.Id == (int)DiscourseChannelKeys.OsebniStik);
                case "#gos.K.P":
                    return await dbContext.DiscourseChannels.SingleAsync(x => x.Id == (int)DiscourseChannelKeys.Telefon);
                case "#gos.K.I":
                    return await dbContext.DiscourseChannels.SingleAsync(x => x.Id == (int)DiscourseChannelKeys.Internet);
                default:
                    throw new Exception($"Invalid discourse channel {channel}!");
            }
        }

        private async Task<DiscourseEvent> GetDiscourseEvent(XElement eventEl)
        {
            var @event = eventEl.Value;

            // GosVL
            if (@event.StartsWith("javno predavanje,", StringComparison.OrdinalIgnoreCase))
            {
                return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Predavanje);
            }

            // Gos
            switch (@event)
            {
                case "PopTV, 24ur":
                case "PopTV, Preverjeno":
                case "TVSlo, Dnevnik":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.NovinarskiPrispevek);
                case "PopTV, Trenja":
                case "PopTV, predvolilna":
                case "TVSlo, Odmevi":
                case "TVSlo, Polemika":
                case "TVSlo, StudioCity":
                case "TVSlo, Tarca": // Not in use
                case "TVSlo, NLP":
                case "TVSlo, Piramida":
                case "PopTV, As ti tud":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ModeriraniPogovorTv);
                case "PopTV, Vzemi ali pusti":
                case "PopTV, Desetka":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ModeriranaOddaja);
                case "PopTV, Kmetija":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ResnicnostniSov);
                case "TVSlo, šport":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.SportniPrenos);
                case "Maribor1, jutranji":
                case "Maribor1, Tribuna":
                case "City, jutranji":
                case "Fantasy, jutranji":
                case "Koroški, Salter":
                case "Aktual, Aktualno":
                case "Belvi, jutranji":
                case "Capris, jutranji":
                case "Krka, potovanja":
                case "Center, jutranji":
                case "Maxi, Pod brajdami":
                case "Maxi, Pižama bar":
                case "Val202, Aktualna":
                case "Val202, Vroči mikrofon":
                case "Ognjišče, Pogovor o":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ModeriraniProgram);
                case "Koroški, intervju":
                case "Kranj, intervju":
                case "Krka, intervju":
                case "Ognjišče, Naš gost":
                case "Štajerski, intervju":
                case "Slovenija1, Intervju":
                case "Aktual, Na sredi":
                case "Val202, Na sceni":
                case "moderirani pogovor":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ModeriraniPogovorRadio);
                case "2. triletje, družboslovje":
                case "2. triletje, naravoslovje":
                case "3. triletje, družboslovje":
                case "3. triletje, naravoslovje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.OsnovnosolskaUcnaUra);
                case "gimnazija, družboslovje":
                case "gimnazija, družb":
                case "gimnazija, naravoslovje":
                case "gimnazija, narav":
                case "poklicna strokovna, družboslovje":
                case "poklicna-strokovna, družb":
                case "poklicna strokovna, naravoslovje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.SrednjesolskaUcnaUra);
                case "jezikovni tečaj za tujce":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Tecaj);
                case "akademski, naravoslovje":
                case "akademski, družboslovje":
                case "akademski, humanistika":
                case "akademski, tehnika":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.FakultetnoPredavanje);
                case "javno predavanje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.JavnoPredavanje);
                case "formalni delovni sestanek":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.FormalniDelovniSestanek);
                case "neformalni delovni sestanek":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.NeformalniDelovniSestanek);
                case "konzultacija":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Konzultacija);
                case "storitev":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Storitev);
                case "formalni razgovor":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.FormalniRazgovor);
                case "prodaja/trgovina":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ProdajaTrgovina);
                case "svetovanje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Svetovanje);
                case "informacije":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Informacije);
                case "tajništvo":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Tajnistvo);
                case "doma, družina":
                case "zunaj doma, družina":
                case "klic sorodniku":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.PogovorVDruzini);
                case "doma, prijatelji":
                case "zunaj doma, prijatelji":
                case "klic prijatelju":
                case "delovno mesto, sodelavci":
                case "klic sodelavcu": // Not in use
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.PogovorMedPrijateljiZnanci);

                // Artur
                case "seja državnega zbora":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.SejaDrzavnegaZbora);
                case "okrogla miza":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.OkroglaMiza);
                case "intervju":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Intervju);
                case "nagovor na dogodku":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.NagovorNaDogodku);
                case "novinarska konferenca":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.NovinarskaKonferenca);
                case "spletni dogodek":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.SpletniDogodek);
                case "seminar":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Seminar);
                case "prosti dialog med dvema sogovornikoma":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ProstiDialogMedDvemaSogovornikoma);
                case "prosti monološki govor":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.ProstiMonoloskiGovor);
                case "predavanje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.Predavanje);
                case "razlaganje in opisovanje":
                    return await dbContext.DiscourseEvents.SingleAsync(x => x.Id == (int)DiscourseEventKeys.RazlaganjeInOpisovanje);
                default:
                    throw new Exception($"Invalid discourse event {@event}!");
            }
        }

        private async Task<DiscourseType> GetDiscourseType(XElement typeEl)
        {
            var type = typeEl.Attribute("target").Value;
            switch (type)
            {
                case "#gos.T.J.I":
                    return await dbContext.DiscourseTypes.SingleAsync(x => x.Id == (int)DiscourseTypeKeys.JavniInformativnoIzobrazevalni);
                case "#gos.T.J.R":
                    return await dbContext.DiscourseTypes.SingleAsync(x => x.Id == (int)DiscourseTypeKeys.JavniRazvedrilni);
                case "#gos.T.N.N":
                    return await dbContext.DiscourseTypes.SingleAsync(x => x.Id == (int)DiscourseTypeKeys.NejavniNezasebni);
                case "#gos.T.N.Z":
                    return await dbContext.DiscourseTypes.SingleAsync(x => x.Id == (int)DiscourseTypeKeys.NejavniZasebni);
                default:
                    throw new Exception($"Invalid discourse type {type}!");
            }
        }

        private void ImportFileDesc(XElement fileDescEl, Discourse discourse)
        {
            // Code
            var titleEl = fileDescEl.Element(Constants.TeiNs + "titleStmt").Element(Constants.TeiNs + "title");

            // Description
            discourse.Description = titleEl.Value;

            // Source
            var biblEl = fileDescEl.Element(Constants.TeiNs + "sourceDesc").Element(Constants.TeiNs + "bibl");
            var publisherEl = biblEl.Element(Constants.TeiNs + "publisher");
            discourse.Source = publisherEl?.Value;

            // Date
            var dateEl = biblEl.Element(Constants.TeiNs + "date");
            var dateParts = dateEl.Value.Split("-");
            discourse.Date = new DateTime(int.Parse(dateParts[0]), int.Parse(dateParts[1]), 1, 0, 0, 0, DateTimeKind.Utc);
        }

        private async Task ImportHeader(XElement headerEl, Discourse discourse)
        {
            // fileDesc
            var fileDescEl = headerEl.Element(Constants.TeiNs + "fileDesc");
            if (fileDescEl != null)
            {
                ImportFileDesc(fileDescEl, discourse);
            }

            // profileDesc
            var profileDescEl = headerEl.Element(Constants.TeiNs + "profileDesc");
            if (profileDescEl != null)
            {
                await ImportProfileDesc(profileDescEl, discourse);
            }
        }

        private async Task ImportProfileDesc(XElement profileDescEl, Discourse discourse)
        {
            // Discourse type
            var catRefEles = profileDescEl.Element(Constants.TeiNs + "textClass").Elements(Constants.TeiNs + "catRef");
            var typeEl = catRefEles.First(x => x.Attribute("target").Value.StartsWith("#gos.T"));
            discourse.Type = await GetDiscourseType(typeEl);

            // Discourse channel
            var channelEl = catRefEles.First(x => x.Attribute("target").Value.StartsWith("#gos.K"));
            discourse.Channel = await GetDiscourseChannel(channelEl);

            // Discourse event
            var domainEl = profileDescEl.Element(Constants.TeiNs + "textDesc")?.Element(Constants.TeiNs + "domain");
            discourse.Event = await GetDiscourseEvent(domainEl);

            // Number of speakers
            var listPersonEl = profileDescEl.Element(Constants.TeiNs + "particDesc").Element(Constants.TeiNs + "listPerson");
            discourse.NumberOfSpeakers = listPersonEl.Elements(Constants.TeiNs + "person").Count();
        }
    }
}