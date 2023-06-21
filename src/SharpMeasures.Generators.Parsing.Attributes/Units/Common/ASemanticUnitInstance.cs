namespace SharpMeasures.Generators.Parsing.Attributes.Units.Common;

/// <summary>An abstract <see cref="IUnitInstance"/>.</summary>
internal abstract class ASemanticUnitInstance : IUnitInstance
{
    private string? Name { get; }
    private string? PluralForm { get; }

    /// <summary>Instantiates a <see cref="ASemanticUnitInstance"/>, representing a parsed attribute describing an instance of a unit.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstance.PluralForm" path="/summary"/></param>
    protected ASemanticUnitInstance(string? name, string? pluralForm)
    {
        Name = name;
        PluralForm = pluralForm;
    }

    string? IUnitInstance.Name => Name;
    string? IUnitInstance.PluralForm => PluralForm;
}
