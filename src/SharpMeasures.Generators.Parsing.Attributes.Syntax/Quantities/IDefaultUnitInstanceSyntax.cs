namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface IDefaultUnitInstanceSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the name of the default unit instance.</summary>
    public abstract Location UnitInstance { get; }

    /// <summary>The <see cref="Location"/> of the argument for the symbol representing the default unit instance. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Symbol { get; }
}
