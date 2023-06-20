namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="DerivedUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticDerivedUnitInstance : IDerivedUnitInstance, ISyntacticUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
    new public abstract IDerivedUnitInstanceSyntax Syntax { get; }
}
