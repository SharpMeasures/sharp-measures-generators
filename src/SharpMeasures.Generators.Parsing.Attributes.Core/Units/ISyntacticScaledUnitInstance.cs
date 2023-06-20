namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="ScaledUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticScaledUnitInstance : IScaledUnitInstance, ISyntacticModifiedUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="ScaledUnitInstanceAttribute"/>.</summary>
    new public abstract IScaledUnitInstanceSyntax Syntax { get; }
}
