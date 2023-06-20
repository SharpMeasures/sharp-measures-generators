namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="DisallowNegativeAttribute"/>, with syntactical information.</summary>
public interface ISyntacticDisallowNegative : IDisallowNegative
{
    /// <summary>Provides syntactical information about the parsed <see cref="DisallowNegativeAttribute"/>.</summary>
    public abstract IDisallowNegativeSyntax Syntax { get; }
}
