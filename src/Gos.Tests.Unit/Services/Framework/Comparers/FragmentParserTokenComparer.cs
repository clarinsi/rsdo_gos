using System;
using System.Collections.Generic;
using Gos.Core.Entities;

namespace Gos.Tests.Unit.Services.Framework.Comparers;

public class FragmentParserTokenComparer : IEqualityComparer<Token>
{
    public bool Equals(Token x, Token y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.ConversationalForm == y.ConversationalForm && x.Lemma == y.Lemma && x.Msd == y.Msd && x.StandardForm == y.StandardForm && x.Type == y.Type;
    }

    public int GetHashCode(Token obj)
    {
        return HashCode.Combine(obj.ConversationalForm, obj.DiscourseOrder, obj.Lemma, obj.Msd, obj.Order, obj.Segment, obj.StandardForm, (int)obj.Type);
    }
}