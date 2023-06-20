namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="IncludeUnitInstancesAttribute"/>, with syntactical information.</summary>
public interface ISyntacticIncludeUnitInstances : IIncludeUnitInstances
{
    /// <summary>Provides syntactical information about the parsed <see cref="IncludeUnitInstancesAttribute"/>.</summary>
    public abstract IIncludeUnitInstancesSyntax Syntax { get; }
}
