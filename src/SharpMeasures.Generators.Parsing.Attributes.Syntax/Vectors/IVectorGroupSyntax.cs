namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
public interface IVectorGroupSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the unit that describes the quantity.</summary>
    public abstract Location Unit { get; }
}
