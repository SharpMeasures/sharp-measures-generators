namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="BiasedUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticBiasedUnitInstance : IBiasedUnitInstance, ISyntacticModifiedUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="BiasedUnitInstanceAttribute"/>.</summary>
    new public abstract IBiasedUnitInstanceSyntax Syntax { get; }
}
