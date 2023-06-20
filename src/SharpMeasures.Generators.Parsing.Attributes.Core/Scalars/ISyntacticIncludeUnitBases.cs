namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="IncludeUnitBasesAttribute"/>, with syntactical information.</summary>
public interface ISyntacticIncludeUnitBases : IIncludeUnitBases
{
    /// <summary>Provides syntactical information about the parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
    public abstract IIncludeUnitBasesSyntax Syntax { get; }
}
