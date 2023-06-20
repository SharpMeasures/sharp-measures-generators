namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="ExcludeUnitBasesAttribute"/>, with syntactical information.</summary>
public interface ISyntacticExcludeUnitBases : IExcludeUnitBases
{
    /// <summary>Provides syntactical information about the parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    public abstract IExcludeUnitBasesSyntax Syntax { get; }
}
