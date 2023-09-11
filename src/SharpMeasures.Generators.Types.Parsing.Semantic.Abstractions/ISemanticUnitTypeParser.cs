namespace SharpMeasures.Generators.Types.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Types.Units;

/// <summary>Parses types as units.</summary>
public interface ISemanticUnitTypeParser
{
    /// <summary>Attempts to parse the provided type as a unit.</summary>
    /// <param name="type">The type that represents a unit.</param>
    /// <returns>The parsed unit, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISemanticUnitType? TryParse(ITypeSymbol type);
}
