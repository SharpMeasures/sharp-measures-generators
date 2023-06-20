namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="FixedUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticFixedUnitInstance : IFixedUnitInstance, ISyntacticUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="FixedUnitInstanceAttribute"/>.</summary>
    new public abstract IFixedUnitInstanceSyntax Syntax { get; }
}
