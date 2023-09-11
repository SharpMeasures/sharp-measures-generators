namespace SharpMeasures.Generators.Types.Parsing;

using Microsoft.CodeAnalysis;

using System.Threading.Tasks;

/// <summary>Parses types as units.</summary>
public interface IUnitTypeParser
{
    /// <summary>Attempts to parse the provided type as a unit.</summary>
    /// <param name="type">The type that represents a unit.</param>
    /// <returns>The parsed unit, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract Task<IUnitType?> TryParse(ITypeSymbol type);
}
