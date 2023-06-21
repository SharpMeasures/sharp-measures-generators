namespace SharpMeasures.Generators.Parsing.Attributes.Units.Common;

using Microsoft.CodeAnalysis;

/// <summary>An abstract <see cref="IUnitInstanceSyntax"/>.</summary>
internal abstract class AUnitInstanceSyntax : AAttributeSyntax, IUnitInstanceSyntax
{
    private Location Name { get; }
    private Location PluralForm { get; }

    /// <summary>Instantiates a <see cref="AUnitInstanceSyntax"/>, representing syntactical information about a parsed attribute describing an instance of a unit.</summary>
    /// <param name="attributeName"><inheritdoc cref="IAttributeSyntax.AttributeName" path="/summary"/></param>
    /// <param name="attribute"><inheritdoc cref="IAttributeSyntax.Attribute" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="IUnitInstanceSyntax.Name" path="/summary"/></param>
    /// <param name="pluralForm"><inheritdoc cref="IUnitInstanceSyntax.PluralForm" path="/summary"/></param>
    protected AUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location pluralForm) : base(attributeName, attribute)
    {
        Name = name;
        PluralForm = pluralForm;
    }

    Location IUnitInstanceSyntax.Name => Name;
    Location IUnitInstanceSyntax.PluralForm => PluralForm;
}
