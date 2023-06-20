namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="QuantityProcessAttribute{TResult}"/>, with syntactical information.</summary>
public interface ISyntacticQuantityProcess : IQuantityProcess
{
    /// <summary>Provides syntactical information about the parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
    public abstract IQuantityProcessSyntax Syntax { get; }
}
