namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="IUnitInstanceRecord"/>.</summary>
public interface IUnitInstanceRecordBuilder : IRecordBuilder<IUnitInstanceRecord>
{
    /// <summary>Specifies the name of the unit instance.</summary>
    /// <param name="name">The name of the unit instance.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithName(string? name, ExpressionSyntax syntax);

    /// <summary>Specifies the plural form of the name of the unit instance.</summary>
    /// <param name="pluralForm">The plural form of the name of the unit instance.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithPluralForm(string? pluralForm, ExpressionSyntax syntax);
}
