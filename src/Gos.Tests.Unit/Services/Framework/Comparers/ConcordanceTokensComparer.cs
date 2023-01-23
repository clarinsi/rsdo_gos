using System;
using System.Collections.Generic;
using Gos.Core.Model;

namespace Gos.Tests.Unit.Services.Framework.Comparers;

public class ConcordanceTokensComparer : IEqualityComparer<Concordance>
{
    public bool Equals(Concordance x, Concordance y)
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

        return Equals(x.Token, y.Token) &&
            Equals(x.TokenLeft1, y.TokenLeft1) &&
            Equals(x.TokenLeft10, y.TokenLeft10) &&
            Equals(x.TokenLeft2, y.TokenLeft2) &&
            Equals(x.TokenLeft3, y.TokenLeft3) &&
            Equals(x.TokenLeft4, y.TokenLeft4) &&
            Equals(x.TokenLeft5, y.TokenLeft5) &&
            Equals(x.TokenLeft6, y.TokenLeft6) &&
            Equals(x.TokenLeft7, y.TokenLeft7) &&
            Equals(x.TokenLeft8, y.TokenLeft8) &&
            Equals(x.TokenLeft9, y.TokenLeft9) &&
            Equals(x.TokenRight1, y.TokenRight1) &&
            Equals(x.TokenRight10, y.TokenRight10) &&
            Equals(x.TokenRight2, y.TokenRight2) &&
            Equals(x.TokenRight3, y.TokenRight3) &&
            Equals(x.TokenRight4, y.TokenRight4) &&
            Equals(x.TokenRight5, y.TokenRight5) &&
            Equals(x.TokenRight6, y.TokenRight6) &&
            Equals(x.TokenRight7, y.TokenRight7) &&
            Equals(x.TokenRight8, y.TokenRight8) &&
            Equals(x.TokenRight9, y.TokenRight9);
    }

    public int GetHashCode(Concordance obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Token);
        hashCode.Add(obj.TokenLeft1);
        hashCode.Add(obj.TokenLeft10);
        hashCode.Add(obj.TokenLeft2);
        hashCode.Add(obj.TokenLeft3);
        hashCode.Add(obj.TokenLeft4);
        hashCode.Add(obj.TokenLeft5);
        hashCode.Add(obj.TokenLeft6);
        hashCode.Add(obj.TokenLeft7);
        hashCode.Add(obj.TokenLeft8);
        hashCode.Add(obj.TokenLeft9);
        hashCode.Add(obj.TokenRight1);
        hashCode.Add(obj.TokenRight10);
        hashCode.Add(obj.TokenRight2);
        hashCode.Add(obj.TokenRight3);
        hashCode.Add(obj.TokenRight4);
        hashCode.Add(obj.TokenRight5);
        hashCode.Add(obj.TokenRight6);
        hashCode.Add(obj.TokenRight7);
        hashCode.Add(obj.TokenRight8);
        hashCode.Add(obj.TokenRight9);
        return hashCode.ToHashCode();
    }
}