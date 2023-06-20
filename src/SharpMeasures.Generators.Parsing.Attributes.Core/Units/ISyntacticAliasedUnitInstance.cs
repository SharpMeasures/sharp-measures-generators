namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
public interface ISyntacticAliasedUnitInstance : IAliasedUnitInstance, ISyntacticModifiedUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="UnitInstanceAliasAttribute"/>.</summary>
    new public abstract IAliasedUnitInstanceSyntax Syntax { get; }
}
