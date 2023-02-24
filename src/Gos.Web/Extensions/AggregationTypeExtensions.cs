using System;
using Gos.ServiceModel.Enums;

namespace Gos.Web.Extensions
{
    public static class AggregationTypeExtensions
    {
        public static string ToQueryStringKey(this AggregationType type)
        {
            return type switch
            {
                AggregationType.DiscourseType => "DiscourseTypeIds",
                AggregationType.DiscourseChannel => "DiscourseChannelIds",
                AggregationType.DiscourseEvent => "DiscourseEventIds",
                AggregationType.DiscourseYear => "DiscourseYears",
                AggregationType.SpeakerAge => "SpeakerAgeIds",
                AggregationType.SpeakerEducation => "SpeakerEducationIds",
                AggregationType.SpeakerLanguage => "SpeakerLanguageIds",
                AggregationType.SpeakerRegion => "SpeakerRegionIds",
                AggregationType.SpeakerSex => "SpeakerSexIds",
                AggregationType.PartOfSpeech => "PartOfSpeechIds",
                AggregationType.Lemma => "Lemmas",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}