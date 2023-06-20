namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>, with syntactical information.</summary>
public interface ISyntacticQuantityOperation : IQuantityOperation
{
    /// <summary>Provides syntactical information about the parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    public abstract IQuantityOperationSyntax Syntax { get; }
}
