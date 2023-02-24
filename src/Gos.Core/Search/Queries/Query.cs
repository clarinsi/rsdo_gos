using System;
using System.Collections.Generic;
using Gos.Core.Extensions;

namespace Gos.Core.Search.Queries
{
    public abstract class Query : ICloneable
    {
        public List<int> DiscourseTypeIds { get; set; }

        public List<int> DiscourseChannelIds { get; set; }

        public List<int> DiscourseEventIds { get; set; }

        public List<int> DiscourseYears { get; set; }

        public int From { get; set; } = 0;

        public int Size { get; set; } = 20;

        public List<int> SpeakerAgeIds { get; set; }

        public List<int> SpeakerEducationIds { get; set; }

        public List<int> SpeakerLanguageIds { get; set; }

        public List<int> SpeakerRegionIds { get; set; }

        public List<int> SpeakerSexIds { get; set; }

        public object Clone()
        {
            return this.Copy();
        }
    }
}