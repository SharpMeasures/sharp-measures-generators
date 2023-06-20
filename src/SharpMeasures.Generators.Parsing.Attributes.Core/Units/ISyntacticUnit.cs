namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="UnitAttribute{TScalar}"/>, with syntactical information.</summary>
public interface ISyntacticUnit : IUnit
{
    /// <summary>Provides syntactical information about the parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
    public abstract IUnitSyntax Syntax { get; }
}
