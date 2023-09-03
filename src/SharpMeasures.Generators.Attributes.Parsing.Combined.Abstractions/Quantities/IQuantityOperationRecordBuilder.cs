namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="IQuantityOperationRecord"/>.</summary>
public interface IQuantityOperationRecordBuilder : IRecordBuilder<IQuantityOperationRecord>
{
    /// <summary>Specifies the quantity that is the result of the operation.</summary>
    /// <param name="result">The quantity that is the result of the operation.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithResult(ITypeSymbol result, ExpressionSyntax syntax);

    /// <summary>Specifies the other quantity in the operation.</summary>
    /// <param name="other">The other quantity in the operation.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOther(ITypeSymbol other, ExpressionSyntax syntax);

    /// <summary>Specifies the operator that is applied to the quantities.</summary>
    /// <param name="operatorType">The operator that is applied to the quantities.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithOperatorType(OperatorType operatorType, ExpressionSyntax syntax);

    /// <summary>Specifies the position of the implementing quantity in the operation.</summary>
    /// <param name="position">The position of the implementing quantity in the operation.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithPosition(OperationPosition position, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies whether the mirrored operation is also implemented.</summary>
    /// <param name="mirrorMode">Determines whether the mirrored operation is also implemented.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithMirrorMode(OperationMirrorMode mirrorMode, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies how the operation is implemented.</summary>
    /// <param name="implementation">Dictates how the operation is implemented.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithImplementation(OperationImplementation implementation, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies how the mirrored operation is implemented, if it is implemented.</summary>
    /// <param name="mirroredImplementation">Dictates how the mirrored operation is implemented, if it is implemented.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithMirroredImplementation(OperationImplementation mirroredImplementation, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the instance method.</summary>
    /// <param name="methodName">The name of the instance method.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithMethodName(string? methodName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the static method.</summary>
    /// <param name="staticMethodName">The name of the static method.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithStaticMethodName(string? staticMethodName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the mirrored instance method.</summary>
    /// <param name="mirroredMethodName">The name of the mirrored instance method.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithMirroredMethodName(string? mirroredMethodName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the mirrored static method.</summary>
    /// <param name="mirroredStaticMethodName">The name of the mirrored static method.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithMirroredStaticMethodName(string? mirroredStaticMethodName, OneOf<None, ExpressionSyntax> syntax);
}
