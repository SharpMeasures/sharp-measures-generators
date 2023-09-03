namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents the arguments of a <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface ISemanticQuantityOperationRecord
{
    /// <summary>The quantity that is the result of the operation.</summary>
    public abstract ITypeSymbol Result { get; }

    /// <summary>The other quantity in the operation.</summary>
    public abstract ITypeSymbol Other { get; }

    /// <summary>The operator that is applied to the quantities.</summary>
    public abstract OperatorType OperatorType { get; }

    /// <summary>The position of the implementing quantity in the operation.</summary>
    public abstract OneOf<None, OperationPosition> Position { get; }

    /// <summary>Determines whether the mirrored operation is also implemented.</summary>
    public abstract OneOf<None, OperationMirrorMode> MirrorMode { get; }

    /// <summary>Determines how the operation is implemented.</summary>
    public abstract OneOf<None, OperationImplementation> Implementation { get; }

    /// <summary>Determines how the mirrored operation is implemented, if it is implemented.</summary>
    public abstract OneOf<None, OperationImplementation> MirroredImplementation { get; }

    /// <summary>The name of the instance method, if implemented.</summary>
    public abstract OneOf<None, string?> MethodName { get; }

    /// <summary>The name of the static method, if implemented.</summary>
    public abstract OneOf<None, string?> StaticMethodName { get; }

    /// <summary>The name of the mirrored instance method, if implemented.</summary>
    public abstract OneOf<None, string?> MirroredMethodName { get; }

    /// <summary>The name of the mirrored static method, if implemented.</summary>
    public abstract OneOf<None, string?> MirroredStaticMethodName { get; }
}
