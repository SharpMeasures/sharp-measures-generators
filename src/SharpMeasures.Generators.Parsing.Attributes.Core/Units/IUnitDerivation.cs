namespace SharpMeasures.Generators.Parsing.Attributes.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="UnitDerivationAttribute"/>.</summary>
public interface IUnitDerivation
{
    /// <summary>The <see cref="string"/> ID of the derivation, if provided.</summary>
    public abstract string? DerivationID { get; }

    /// <summary>The <see cref="string"/> expression used to derive instances of the unit. Occurrences of "{k}" are replaced by the k-th unit in the provided signature.</summary>
    public abstract string? Expression { get; }

    /// <summary>The signature of the derivation, a collection of units.</summary>
    public abstract IReadOnlyList<ITypeSymbol?>? Signature { get; }

    /// <summary>The name of the method deriving the unit, if provided.</summary>
    public abstract string? MethodName { get; }
}
