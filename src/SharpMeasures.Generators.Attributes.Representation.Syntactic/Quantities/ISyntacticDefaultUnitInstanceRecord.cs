namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface ISyntacticDefaultUnitInstanceRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the name of the default unit instance.</summary>
    public abstract ExpressionSyntax UnitInstance { get; }

    /// <summary>The syntactic description of the argument for the symbol representing the default unit instance.</summary>
    public abstract OneOf<None, ExpressionSyntax> Symbol { get; }
}
