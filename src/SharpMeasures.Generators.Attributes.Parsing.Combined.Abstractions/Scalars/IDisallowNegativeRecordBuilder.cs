namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="IDisallowNegativeRecord"/>.</summary>
public interface IDisallowNegativeRecordBuilder : IRecordBuilder<IDisallowNegativeRecord>
{
    /// <summary>Specifies the behaviour of the constructor of the quantity.</summary>
    /// <param name="behaviour">The behaviour of the constructor of the quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBehaviour(DisallowNegativeBehaviour behaviour, ExpressionSyntax syntax);
}
