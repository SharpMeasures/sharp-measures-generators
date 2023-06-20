namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface IScalarQuantitySyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the unit that describes the quantity.</summary>
    public abstract Location Unit { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity is biased. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Biased { get; }
}
