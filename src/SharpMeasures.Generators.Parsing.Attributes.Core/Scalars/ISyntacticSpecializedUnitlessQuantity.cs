namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>, with syntactical information.</summary>
public interface ISyntacticSpecializedUnitlessQuantity : ISpecializedUnitlessQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedUnitlessQuantitySyntax Syntax { get; }
}
