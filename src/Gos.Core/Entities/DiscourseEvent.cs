using System.Globalization;

namespace Gos.Core.Entities
{
    public class DiscourseEvent : BaseEntity, ILocalizableEntity<DiscourseEventTranslation>
    {
        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<DiscourseEventTranslation> Translations { get; set; } = new();
    }

    public enum DiscourseEventKeys
    {
        NovinarskiPrispevek = 1,
        ModeriraniPogovorTv = 2,
        ModeriranaOddaja = 3,
        ResnicnostniSov = 4,
        SportniPrenos = 5,
        ModeriraniProgram = 6,
        ModeriraniPogovorRadio = 7,
        OsnovnosolskaUcnaUra = 8,
        SrednjesolskaUcnaUra = 9,
        Tecaj = 10,
        FakultetnoPredavanje = 11,
        JavnoPredavanje = 12,
        FormalniDelovniSestanek = 13,
        NeformalniDelovniSestanek = 14,
        Konzultacija = 15,
        Storitev = 16,
        FormalniRazgovor = 17,
        ProdajaTrgovina = 18,
        Svetovanje = 19,
        Informacije = 20,
        Tajnistvo = 21,
        PogovorVDruzini = 22,
        PogovorMedPrijateljiZnanci = 23,

        // GosVL in Artur
        Predavanje = 24,

        // Artur
        SejaDrzavnegaZbora = 25,
        OkroglaMiza = 26,
        Intervju = 27,
        NagovorNaDogodku = 28,
        NovinarskaKonferenca = 29,
        SpletniDogodek = 30,
        Seminar = 31,
        ProstiDialogMedDvemaSogovornikoma = 32,
        ProstiMonoloskiGovor = 33,
        RazlaganjeInOpisovanje = 34,
    }
}