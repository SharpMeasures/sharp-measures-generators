namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, describing an operation { + , - , ⋅ , ÷, ⨯ } supported by the marked quantity.</summary>
/// <typeparam name="TResult">The quantity that is the result of the operation.</typeparam>
/// <typeparam name="TOther">The other quantity in the operation.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityOperationAttribute<TResult, TOther> : Attribute
{
    /// <summary>The operator that is applied to the quantities.</summary>
    public OperatorType OperatorType { get; }

    /// <summary>The position of the implementing quantity in the operation. The default value is <see cref="OperationPosition.Left"/>.</summary>
    public OperationPosition Position { get; init; }

    /// <summary>Determines whether the mirrored operation is also implemented. The default behaviour is <see cref="OperationMirrorMode.Adaptive"/>.</summary>
    public OperationMirrorMode MirrorMode { get; init; }

    /// <summary>Determines how the operation is implemented. The default behaviour is <see cref="OperationImplementation.All"/>.</summary>
    public OperationImplementation Implementation { get; init; }

    /// <summary>Determines how the mirrored operation is implemented, if it is implemented. The default behaviour is <see cref="OperationImplementation.Operator"/>.</summary>
    public OperationImplementation MirroredImplementation { get; init; }

    /// <summary>The name of the instance method, if implemented. By default, the name will be one of { <i>Add</i>, <i>Subtract</i>, <i>SubtractFrom</i>, <i>Multiply</i>, <i>DivideBy</i>, <i>DivideInto</i>, <i>Dot</i>, <i>Cross</i>, <i>CrossInto</i> } - depending on the <see cref="SharpMeasures.OperatorType"/> and <see cref="OperationPosition"/>.</summary>
    public string? MethodName { get; init; }

    /// <summary>The name of the static method, if implemented. By default, the name will be one of { <i>Add</i>, <i>Subtract</i>, <i>Multiply</i>, <i>Divide</i>, <i>Dot</i>, <i>Cross</i> } - depending on the <see cref="SharpMeasures.OperatorType"/> and <see cref="OperationPosition"/>.</summary>
    public string? StaticMethodName { get; init; }

    /// <summary>The name of the mirrored instance method, if implemented. By default, the name will be one of { <i>Add</i>, <i>Subtract</i>, <i>SubtractFrom</i>, <i>Multiply</i>, <i>DivideBy</i>, <i>DivideInto</i>, <i>Dot</i>, <i>Cross</i>, <i>CrossInto</i> } - depending on the <see cref="SharpMeasures.OperatorType"/> and <see cref="OperationPosition"/>.</summary>
    public string? MirroredMethodName { get; init; }

    /// <summary>The name of the mirrored static method, if implemented. By default, the name will be one of { <i>Add</i>, <i>Subtract</i>, <i>Multiply</i>, <i>Divide</i>, <i>Dot</i>, <i>Cross</i> } - depending on the <see cref="SharpMeasures.OperatorType"/> and <see cref="OperationPosition"/>.</summary>
    public string? MirroredStaticMethodName { get; init; }

    /// <summary>Describes an operation supported by the marked quantity.</summary>
    /// <param name="operatorType"><inheritdoc cref="OperatorType" path="/summary"/></param>
    public QuantityOperationAttribute(OperatorType operatorType)
    {
        OperatorType = operatorType;
    }
}
