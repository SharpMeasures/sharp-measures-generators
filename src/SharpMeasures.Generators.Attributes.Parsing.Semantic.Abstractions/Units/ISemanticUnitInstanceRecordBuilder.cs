namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="ISemanticUnitInstanceRecord"/>.</summary>
public interface ISemanticUnitInstanceRecordBuilder : IRecordBuilder<ISemanticUnitInstanceRecord>
{
    /// <summary>Specifies the name of the unit instance.</summary>
    /// <param name="name">The name of the unit instance.</param>
    public abstract void WithName(string? name);

    /// <summary>Specifies the plural form of the name of the unit instance.</summary>
    /// <param name="pluralForm">The plural form of the name of the unit instance.</param>
    public abstract void WithPluralForm(string? pluralForm);
}
