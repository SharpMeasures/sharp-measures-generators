namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed attribute that describes an instance of a unit as a modified form of another instance, with syntactical information.</summary>
public interface ISyntacticModifiedUnitInstance : IModifiedUnitInstance, ISyntacticUnitInstance
{
    /// <summary>Provides syntactical information about the parsed attribute.</summary>
    new public abstract IModifiedUnitInstanceSyntax Syntax { get; }
}
