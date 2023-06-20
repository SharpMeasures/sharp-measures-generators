namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="ExcludeUnitInstancesAttribute"/>, with syntactical information.</summary>
public interface ISyntacticExcludeUnitInstances : IExcludeUnitInstances
{
    /// <summary>Provides syntactical information about the parsed <see cref="ExcludeUnitInstancesAttribute"/>.</summary>
    public abstract IExcludeUnitInstancesSyntax Syntax { get; }
}
