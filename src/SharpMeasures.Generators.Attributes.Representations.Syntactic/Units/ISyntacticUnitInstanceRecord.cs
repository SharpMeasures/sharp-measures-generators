namespace SharpMeasures.Generators.Attributes.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="UnitInstanceAttribute"/>.</summary>
public interface ISyntacticUnitInstanceRecord
{
    /// <summary>The syntactic description of the argument for the name of the unit instance.</summary>
    public abstract OneOf<None, ExpressionSyntax> Name { get; }

    /// <summary>The syntactic description of the argument for the plural form of the name of the unit instance.</summary>
    public abstract OneOf<None, ExpressionSyntax> PluralForm { get; }
}
