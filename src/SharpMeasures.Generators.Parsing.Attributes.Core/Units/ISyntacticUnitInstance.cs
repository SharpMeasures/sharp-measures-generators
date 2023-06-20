namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed attribute that describes an instance of a unit, with syntactical information.</summary>
public interface ISyntacticUnitInstance : IUnitInstance
{
    /// <summary>Provides syntactical information about the parsed attribute.</summary>
    public abstract IUnitInstanceSyntax Syntax { get; }
}
