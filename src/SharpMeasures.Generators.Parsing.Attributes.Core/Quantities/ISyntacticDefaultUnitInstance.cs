namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

/// <summary>Represents a parsed <see cref="DefaultUnitInstanceAttribute"/>, with syntactical information.</summary>
public interface ISyntacticDefaultUnitInstance : IDefaultUnitInstance
{
    /// <summary>Provides syntactical information about the parsed <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    public abstract IDefaultUnitInstanceSyntax Syntax { get; }
}
