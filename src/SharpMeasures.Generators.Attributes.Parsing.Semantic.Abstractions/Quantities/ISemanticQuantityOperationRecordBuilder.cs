namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="ISemanticQuantityOperationRecord"/>.</summary>
public interface ISemanticQuantityOperationRecordBuilder : IRecordBuilder<ISemanticQuantityOperationRecord>
{
    /// <summary>Specifies the quantity that is the result of the operation.</summary>
    /// <param name="result">The quantity that is the result of the operation.</param>
    public abstract void WithResult(ITypeSymbol result);

    /// <summary>Specifies the other quantity in the operation.</summary>
    /// <param name="other">The other quantity in the operation.</param>
    public abstract void WithOther(ITypeSymbol other);

    /// <summary>Specifies the operator that is applied to the quantities.</summary>
    /// <param name="operatorType">The operator that is applied to the quantities.</param>
    public abstract void WithOperatorType(OperatorType operatorType);

    /// <summary>Specifies the position of the implementing quantity in the operation.</summary>
    /// <param name="position">The position of the implementing quantity in the operation.</param>
    public abstract void WithPosition(OperationPosition position);

    /// <summary>Specifies whether the mirrored operation is also implemented.</summary>
    /// <param name="mirrorMode">Determines whether the mirrored operation is also implemented.</param>
    public abstract void WithMirrorMode(OperationMirrorMode mirrorMode);

    /// <summary>Specifies how the operation is implemented.</summary>
    /// <param name="implementation">Dictates how the operation is implemented.</param>
    public abstract void WithImplementation(OperationImplementation implementation);

    /// <summary>Specifies how the mirrored operation is implemented, if it is implemented.</summary>
    /// <param name="mirroredImplementation">Dictates how the mirrored operation is implemented, if it is implemented.</param>
    public abstract void WithMirroredImplementation(OperationImplementation mirroredImplementation);

    /// <summary>Specifies the name of the instance method.</summary>
    /// <param name="methodName">The name of the instance method.</param>
    public abstract void WithMethodName(string? methodName);

    /// <summary>Specifies the name of the static method.</summary>
    /// <param name="staticMethodName">The name of the static method.</param>
    public abstract void WithStaticMethodName(string? staticMethodName);

    /// <summary>Specifies the name of the mirrored instance method.</summary>
    /// <param name="mirroredMethodName">The name of the mirrored instance method.</param>
    public abstract void WithMirroredMethodName(string? mirroredMethodName);

    /// <summary>Specifies the name of the mirrored static method.</summary>
    /// <param name="mirroredStaticMethodName">The name of the mirrored static method.</param>
    public abstract void WithMirroredStaticMethodName(string? mirroredStaticMethodName);
}
