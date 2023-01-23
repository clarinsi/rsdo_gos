using System.Collections.Generic;
using MediatR;
using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Requests
{
    public abstract class Search<TResponse> : Search, IRequest<TResponse>
    {
    }

    public abstract class Search
    {
        public List<int> DiscourseTypeIds { get; set; } = new();

        public List<int> DiscourseChannelIds { get; set; } = new();

        public List<int> DiscourseEventIds { get; set; } = new();

        public List<int> DiscourseRegionIds { get; set; } = new();

        public List<int> DiscourseYears { get; set; } = new();

        public string Query { get; set; }

        public SortDirection SortDirection { get; set; }

        public string SortField { get; set; }

        public List<int> SpeakerAgeIds { get; set; } = new();

        public List<int> SpeakerEducationIds { get; set; } = new();

        public List<int> SpeakerLanguageIds { get; set; } = new();

        public List<int> SpeakerRegionIds { get; set; } = new();

        public List<int> SpeakerSexIds { get; set; } = new();

        public TranscriptionType TranscriptionType { get; set; } = TranscriptionType.Conversational;
    }
}