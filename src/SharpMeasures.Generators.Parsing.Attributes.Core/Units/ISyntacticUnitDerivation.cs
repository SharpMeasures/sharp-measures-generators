namespace SharpMeasures.Generators.Parsing.Attributes.Units;

/// <summary>Represents a parsed <see cref="UnitDerivationAttribute"/>, with syntactical information.</summary>
public interface ISyntacticUnitDerivation : IUnitDerivation
{
    /// <summary>Provides syntactical information about the parsed <see cref="UnitDerivationAttribute"/>.</summary>
    public abstract IUnitDerivationSyntax Syntax { get; }
}
