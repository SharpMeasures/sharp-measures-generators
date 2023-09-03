namespace SharpMeasures.Generators.Attributes.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface ISyntacticUnitRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the scalar quantity primarily described by the unit.</summary>
    public abstract ExpressionSyntax ScalarQuantity { get; }

    /// <summary>The syntactic description of the argument determining whether the unit includes a bias term.</summary>
    public abstract OneOf<None, ExpressionSyntax> BiasTerm { get; }
}
