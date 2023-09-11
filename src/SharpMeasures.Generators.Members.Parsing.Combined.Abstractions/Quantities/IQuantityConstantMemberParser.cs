﻿namespace SharpMeasures.Generators.Members.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Members.Quantities;

using System.Threading.Tasks;

/// <summary>Parses members of SharpMeasures quantities as constants.</summary>
public interface IQuantityConstantMemberParser
{
    /// <summary>Attempts to parse the provided property as a constant.</summary>
    /// <param name="property">The property that defines the constant.</param>
    /// <param name="quantityType">The quantity that defines the constant.</param>
    /// <returns>The parsed constant, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract Task<IQuantityConstantMember?> TryParse(IPropertySymbol property, ITypeSymbol quantityType);
}
