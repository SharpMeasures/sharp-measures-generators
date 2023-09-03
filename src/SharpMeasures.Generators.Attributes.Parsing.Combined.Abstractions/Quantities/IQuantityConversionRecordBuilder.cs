namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

using System.Collections.Generic;

/// <summary>Handles incremental construction of <see cref="IQuantityConversionRecord"/>.</summary>
public interface IQuantityConversionRecordBuilder : IRecordBuilder<IQuantityConversionRecord>
{
    /// <summary>Specifies the set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    /// <param name="quantities">The set of quantities to and/or from which the implementing quantity supports conversion.</param>
    /// <param name="syntax">The syntactic description of the argument, or of each element in the argument if expressed as a <see langword="params"/>-array.</param>
    public abstract void WithQuantities(IReadOnlyList<ITypeSymbol?>? quantities, OneOf<ExpressionSyntax, IReadOnlyList<ExpressionSyntax>> syntax);

    /// <summary>Specifies how the conversions from the implementing quantity to the provided quantities are implemented.</summary>
    /// <param name="forwardsImplementation">Determines how the conversions from the implementing quantity to the provided quantities are implemented.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithForwardsImplementation(ConversionImplementation forwardsImplementation, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the behaviour of the operators converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsBehaviour">The behaviour of the operators converting from the implementing quantity to the provided quantities.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the property converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsPropertyName">The name of the property converting from the implementing quantity to the provided quantities.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithForwardsPropertyName(string? forwardsPropertyName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the instance methods converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsMethodName">The name of the instance methods converting from the implementing quantity to the provided quantities.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithForwardsMethodName(string? forwardsMethodName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the static method converting from the implementing quantity to the provided quantities.</summary>
    /// <param name="forwardsStaticMethodName">The name of the static method converting from the implementing quantity to the provided quantities.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithForwardsStaticMethodName(string? forwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies how the conversions from the provided quantities to the implementing quantity are implemented.</summary>
    /// <param name="backwardsImplementation">Determines how the conversions from the provided quantities to the implementing quantity are implemented.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBackwardsImplementation(ConversionImplementation backwardsImplementation, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the behaviour of the operators converting from the provided quantities to the implementing quantity.</summary>
    /// <param name="backwardsBehaviour">The behaviour of the operators converting from the provided quantities to the implementing quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour, OneOf<None, ExpressionSyntax> syntax);

    /// <summary>Specifies the name of the static method converting from the provided quantities to the implementing quantity.</summary>
    /// <param name="backwardsStaticMethodName">The name of the static method converting from the provided quantities to the implementing quantity.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    public abstract void WithBackwardsStaticMethodName(string? backwardsStaticMethodName, OneOf<None, ExpressionSyntax> syntax);
}
