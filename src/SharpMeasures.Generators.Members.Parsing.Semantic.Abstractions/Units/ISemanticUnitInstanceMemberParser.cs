namespace SharpMeasures.Generators.Members.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Members.Units;

/// <summary>Parses members of SharpMeasures units as unit instances.</summary>
public interface ISemanticUnitInstanceMemberParser
{
    /// <summary>Attempts to parse the provided property as a unit instance.</summary>
    /// <param name="property">The property that defines the unit instance.</param>
    /// <param name="unitType">The unit that defines the unit instance.</param>
    /// <returns>The parsed unit instance, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISemanticUnitInstanceMember? TryParse(IPropertySymbol property, ITypeSymbol unitType);
}
