namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorConstantCases;

using Microsoft.CodeAnalysis;

using OneOf;

using System;
using System.Collections.Generic;
using System.Linq;

internal sealed class ValueEqualityComparer : IEqualityComparer<OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>>
{
    public static IEqualityComparer<OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>> Comparer { get; } = new ValueEqualityComparer();

    private ValueEqualityComparer() { }

    bool IEqualityComparer<OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>>.Equals(OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> x, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> y)
    {
        if (x.IsT0)
        {
            if (y.IsT0 is false)
            {
                return false;
            }

            return CompareDoubleCollections(x.AsT0, y.AsT0);
        }

        if (y.IsT1 is false)
        {
            return false;
        }

        return CompareStringCollections(x.AsT1, y.AsT1);
    }

    int IEqualityComparer<OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>>.GetHashCode(OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> obj) => 0;

    private static bool CompareDoubleCollections(IReadOnlyList<double>? x, IReadOnlyList<double>? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        if (x.Count != y.Count)
        {
            return false;
        }

        return Enumerable.Zip(x, y).All(static (values) => values.First == values.Second);
    }

    private static bool CompareStringCollections(IReadOnlyList<string?>? x, IReadOnlyList<string?>? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        if (x.Count != y.Count)
        {
            return false;
        }

        return Enumerable.Zip(x, y).All(static (values) => StringComparer.Ordinal.Equals(values.First, values.Second));
    }
}
