﻿using System.Globalization;

namespace Gos.Core.Entities
{
    public class SpeakerRegion : BaseEntity, ILocalizableEntity<SpeakerRegionTranslation>
    {
        public string ShortTitle
        {
            get => Translations[CultureInfo.CurrentCulture].ShortTitle;
            set => Translations[CultureInfo.CurrentCulture].ShortTitle = value;
        }

        public string Title
        {
            get => Translations[CultureInfo.CurrentCulture].Title;
            set => Translations[CultureInfo.CurrentCulture].Title = value;
        }

        public TranslationCollection<SpeakerRegionTranslation> Translations { get; set; } = new();
    }

    public enum SpeakerRegionKeys
    {
        Savinjska = 1,
        Goriska = 2,
        Posavska = 3,
        ObalnoKraska = 4,
        Gorenjska = 5,
        Osrednjeslovenska = 6,
        Podravska = 7,
        Pomurska = 8,
        JugovzhodnaSlovenija = 9,
        Koroška = 10,
        Zasavska = 11,
        PrimorskoNotranjska = 12,
        Avstrija = 13,
        Italija = 14,
        Madzarska = 15,
        Tujina = 16,
        Neznano = 17,
    }
}