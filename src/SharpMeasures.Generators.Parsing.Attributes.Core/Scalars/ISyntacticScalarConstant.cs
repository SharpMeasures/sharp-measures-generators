namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="ScalarConstantAttribute"/>, with syntactical information.</summary>
public interface ISyntacticScalarConstant : IScalarConstant
{
    /// <summary>Provides syntactical information about the parsed <see cref="ScalarConstantAttribute"/>.</summary>
    public abstract IScalarConstantSyntax Syntax { get; }
}
