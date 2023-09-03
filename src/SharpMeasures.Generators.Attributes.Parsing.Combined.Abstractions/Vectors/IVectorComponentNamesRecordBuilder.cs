namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Handles incremental construction of <see cref="IVectorComponentNamesRecord"/>.</summary>
public interface IVectorComponentNamesRecordBuilder : IRecordBuilder<IVectorComponentNamesRecord>
{
    /// <summary>Specifies the names of the Cartesian components.</summary>
    /// <param name="names">The names of the Cartesian components.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithNames(IReadOnlyList<string?>? names, ExpressionSyntax syntax);

    /// <summary>Specifies the expression used to derive the name of each Cartesian component.</summary>
    /// <param name="expression">The expression used to derive the name of each Cartesian component.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithExpression(string? expression, ExpressionSyntax syntax);
}
