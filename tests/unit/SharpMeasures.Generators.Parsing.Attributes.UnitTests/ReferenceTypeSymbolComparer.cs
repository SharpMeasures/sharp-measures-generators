namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

internal static class ReferenceTypeSymbolComparer
{
    public static IEqualityComparer<ITypeSymbol?> IndividualComparer { get; } = new IndividualImplementation();
    public static IEqualityComparer<IEnumerable<ITypeSymbol?>?> CollectionComparer { get; } = new CollectionImplementation();

    private sealed class IndividualImplementation : IEqualityComparer<ITypeSymbol?>
    {
        bool IEqualityComparer<ITypeSymbol?>.Equals(ITypeSymbol? x, ITypeSymbol? y) => ReferenceEquals(x, y);

        int IEqualityComparer<ITypeSymbol?>.GetHashCode(ITypeSymbol? obj) => 0;
    }

    private sealed class CollectionImplementation : IEqualityComparer<IEnumerable<ITypeSymbol?>?>
    {
        bool IEqualityComparer<IEnumerable<ITypeSymbol?>?>.Equals(IEnumerable<ITypeSymbol?>? x, IEnumerable<ITypeSymbol?>? y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            var xEnumerator = x.GetEnumerator();
            var yEnumerator = y.GetEnumerator();

            while (true)
            {
                var xDone = xEnumerator.MoveNext() is false;
                var yDone = yEnumerator.MoveNext() is false;

                if (xDone && yDone)
                {
                    return true;
                }

                if (xDone || yDone)
                {
                    return false;
                }

                if (IndividualComparer.Equals(xEnumerator.Current, yEnumerator.Current) is false)
                {
                    return false;
                }
            }
        }

        int IEqualityComparer<IEnumerable<ITypeSymbol?>?>.GetHashCode(IEnumerable<ITypeSymbol?>? obj) => 0;
    }
}
