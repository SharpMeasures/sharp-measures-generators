namespace SharpMeasures.Generators.Parsing.Attributes.Units.Common;

/// <summary>An abstract <see cref="IModifiedUnitInstance"/>.</summary>
internal abstract class ASemanticModifiedUnitInstance : ASemanticUnitInstance, IModifiedUnitInstance
{
    private string? OriginalUnitInstance { get; }

    /// <summary>Instantiates a <see cref="ASemanticModifiedUnitInstance"/>, representing a parsed attribute describing a unit instance as a modified form of another unit instance.</summary>
    /// <param name="name"><inheritdoc cref="IUnitInstance.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstance.PluralForm" path="/summary"/></param>
    /// <param name="originalUnitInstance"><inheritdoc cref="IModifiedUnitInstance.OriginalUnitInstance" path="/summary"/></param>
    protected ASemanticModifiedUnitInstance(string? name, string? pluralForm, string? originalUnitInstance) : base(name, pluralForm)
    {
        OriginalUnitInstance = originalUnitInstance;
    }

    string? IModifiedUnitInstance.OriginalUnitInstance => OriginalUnitInstance;
}
