namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="UnitlessQuantityAttribute"/>, with syntactical information.</summary>
public interface ISyntacticUnitlessQuantity : IUnitlessQuantity
{
    /// <summary>Provides syntactical information about the parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
    public abstract IUnitlessQuantitySyntax Syntax { get; }
}
