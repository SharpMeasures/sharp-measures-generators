namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>, with syntactical information.</summary>
public interface ISyntacticSpecializedScalarQuantity : ISpecializedScalarQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedScalarQuantitySyntax Syntax { get; }
}
