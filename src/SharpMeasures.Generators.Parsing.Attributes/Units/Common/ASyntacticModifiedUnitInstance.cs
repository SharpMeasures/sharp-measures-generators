namespace SharpMeasures.Generators.Parsing.Attributes.Units.Common;

/// <summary>An abstract <see cref="IModifiedUnitInstance"/>.</summary>
/// <typeparam name="TDefinition">The type of the described <see cref="IModifiedUnitInstance"/>.</typeparam>
/// <typeparam name="TSyntax">The type of the described <see cref="IModifiedUnitInstanceSyntax"/>.</typeparam>
internal abstract class ASyntacticModifiedUnitInstance<TDefinition, TSyntax> : ASyntacticUnitInstance<TDefinition, TSyntax>, ISyntacticModifiedUnitInstance where TDefinition : IModifiedUnitInstance where TSyntax : IModifiedUnitInstanceSyntax
{
    /// <summary>Instantiates a <see cref="ASyntacticModifiedUnitInstance{TDefinition, TSyntax}"/>, representing a parsed attribute describing a unit instance as a modified form of another unit instance.</summary>
    /// <param name="semantics">Semantically describes the parsed attribute</param>
    /// <param name="syntax"><inheritdoc cref="ISyntacticModifiedUnitInstance.Syntax" path="/summary"/></param>
    protected ASyntacticModifiedUnitInstance(TDefinition semantics, TSyntax syntax) : base(semantics, syntax) { }

    string? IModifiedUnitInstance.OriginalUnitInstance => Semantics.OriginalUnitInstance;

    IModifiedUnitInstanceSyntax ISyntacticModifiedUnitInstance.Syntax => Syntax;
}
