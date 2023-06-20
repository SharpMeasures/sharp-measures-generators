namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="DisallowNegativeAttribute"/>.</summary>
public interface IDisallowNegativeSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the behaviour of the quantity constructor. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Behaviour { get; }
}
