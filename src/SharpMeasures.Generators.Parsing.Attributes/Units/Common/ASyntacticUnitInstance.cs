namespace SharpMeasures.Generators.Parsing.Attributes.Units.Common;

/// <summary>An abstract <see cref="ISyntacticUnitInstance"/>.</summary>
/// <typeparam name="TDefinition">The type of the described <see cref="IUnitInstance"/>.</typeparam>
/// <typeparam name="TSyntax">The type of the described <see cref="IUnitInstanceSyntax"/>.</typeparam>
internal abstract class ASyntacticUnitInstance<TDefinition, TSyntax> : ISyntacticUnitInstance where TDefinition : IUnitInstance where TSyntax : IUnitInstanceSyntax
{
    /// <summary>Provides semantical information about the parsed attribute.</summary>
    protected TDefinition Semantics { get; }

    /// <summary>Provides syntactical information about the parsed attribute.</summary>
    protected TSyntax Syntax { get; }

    /// <summary>Instantiates a <see cref="ASyntacticUnitInstance{TDefinition, TSyntax}"/>, representing a parsed attribute describing an instance of a unit.</summary>
    /// <param name="semantics"><inheritdoc cref="Semantics" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="Syntax" path="/summary"/></param>
    protected ASyntacticUnitInstance(TDefinition semantics, TSyntax syntax)
    {
        Semantics = semantics;
        Syntax = syntax;
    }

    string? IUnitInstance.Name => Semantics.Name;
    string? IUnitInstance.PluralForm => Semantics.PluralForm;

    IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
}
