namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="PrefixedUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticPrefixedUnitInstance : IPrefixedUnitInstance, ISyntacticModifiedUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="PrefixedUnitInstanceAttribute"/>.</summary>
    new public abstract IPrefixedUnitInstanceSyntax Syntax { get; }
}
