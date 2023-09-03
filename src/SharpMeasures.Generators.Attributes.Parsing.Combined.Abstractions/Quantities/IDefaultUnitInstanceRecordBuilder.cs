namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="IDefaultUnitInstanceRecord"/>.</summary>
public interface IDefaultUnitInstanceRecordBuilder : IRecordBuilder<IDefaultUnitInstanceRecord>
{
    /// <summary>Specifies the name of the default unit instance.</summary>
    /// <param name="unitInstance">The name of the default unit instance.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithUnitInstance(string? unitInstance, ExpressionSyntax syntax);

    /// <summary>Specifies the symbol of the default unit instance.</summary>
    /// <param name="symbol">The symbol of the default unit instance.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithSymbol(string? symbol, ExpressionSyntax syntax);
}
