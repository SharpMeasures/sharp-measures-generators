namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

/// <summary>Represents syntactic information about a <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface ISyntacticQuantityOperationRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the quantity that is the result of the operation.</summary>
    public abstract ExpressionSyntax Result { get; }

    /// <summary>The syntactic description of the argument for the other quantity in the operation.</summary>
    public abstract ExpressionSyntax Other { get; }

    /// <summary>The syntactic description of the argument for the operator that is applied to the quantities.</summary>
    public abstract ExpressionSyntax OperatorType { get; }

    /// <summary>The syntactic description of the argument for the position of the implementing quantity in the operation.</summary>
    public abstract OneOf<None, ExpressionSyntax> Position { get; }

    /// <summary>The syntactic description of the argument for whether the mirrored operation is also implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> MirrorMode { get; }

    /// <summary>The syntactic description of the argument for how the operation is implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> Implementation { get; }

    /// <summary>The syntactic description of the argument for how the mirrored operation is implemented, if it is implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> MirroredImplementation { get; }

    /// <summary>The syntactic description of the argument for the name of the instance method, if implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> MethodName { get; }

    /// <summary>The syntactic description of the argument for the name of the static method, if implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> StaticMethodName { get; }

    /// <summary>The syntactic description of the argument for the name of the mirrored instance method, if implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> MirroredMethodName { get; }

    /// <summary>The syntactic description of the argument for the name of the mirrored static method, if implemented.</summary>
    public abstract OneOf<None, ExpressionSyntax> MirroredStaticMethodName { get; }
}
