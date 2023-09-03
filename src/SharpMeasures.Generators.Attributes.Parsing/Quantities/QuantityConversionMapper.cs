namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

using System.Collections.Generic;

/// <summary>Maps the parameters of <see cref="QuantityConversionAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class QuantityConversionMapper : AAdaptiveMapper<IQuantityConversionRecordBuilder, ISemanticQuantityConversionRecordBuilder>
{
    /// <summary>Instantiates a <see cref="QuantityConversionMapper"/>, mapping the parameters of <see cref="QuantityConversionAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public QuantityConversionMapper(IAdaptiveMapperDependencyProvider<IQuantityConversionRecordBuilder, ISemanticQuantityConversionRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IQuantityConversionRecordBuilder, ISemanticQuantityConversionRecordBuilder> repository)
    {
        repository.ConstructorParameters.AddNamedMapping(nameof(QuantityConversionAttribute.Quantities), (factory) => factory.Normal.Create(TypeArrayPattern, RecordQuantities, RecordQuantities));

        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.ForwardsImplementation), (factory) => factory.Create(ConversionImplementationPattern, RecordForwardsImplementation, RecordForwardsImplementation));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.ForwardsBehaviour), (factory) => factory.Create(ConversionOperatorBehaviourPattern, RecordForwardsBehaviour, RecordForwardsBehaviour));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.ForwardsPropertyName), (factory) => factory.Create(NullableStringPattern, RecordForwardsPropertyName, RecordForwardsPropertyName));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.ForwardsMethodName), (factory) => factory.Create(NullableStringPattern, RecordForwardsMethodName, RecordForwardsMethodName));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.ForwardsStaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordForwardsStaticMethodName, RecordForwardsStaticMethodName));

        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.BackwardsImplementation), (factory) => factory.Create(ConversionImplementationPattern, RecordBackwardsImplementation, RecordBackwardsImplementation));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.BackwardsBehaviour), (factory) => factory.Create(ConversionOperatorBehaviourPattern, RecordBackwardsBehaviour, RecordBackwardsBehaviour));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityConversionAttribute.BackwardsStaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordBackwardsStaticMethodName, RecordBackwardsStaticMethodName));
    }

    private static IArgumentPattern<ITypeSymbol?[]?> TypeArrayPattern(IArgumentPatternFactory factory) => factory.NullableArray(factory.NullableType());
    private static IArgumentPattern<ConversionImplementation> ConversionImplementationPattern(IArgumentPatternFactory factory) => factory.Enum<ConversionImplementation>();
    private static IArgumentPattern<ConversionOperatorBehaviour> ConversionOperatorBehaviourPattern(IArgumentPatternFactory factory) => factory.Enum<ConversionOperatorBehaviour>();
    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordQuantities(IQuantityConversionRecordBuilder recordBuilder, IReadOnlyList<ITypeSymbol?>? quantities, ExpressionSyntax syntax) => recordBuilder.WithQuantities(quantities, syntax);
    private static void RecordQuantities(ISemanticQuantityConversionRecordBuilder recordBuilder, IReadOnlyList<ITypeSymbol?>? quantities) => recordBuilder.WithQuantities(quantities);

    private static void RecordForwardsImplementation(IQuantityConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImplementation, ExpressionSyntax syntax) => recordBuilder.WithForwardsImplementation(forwardsImplementation, syntax);
    private static void RecordForwardsImplementation(ISemanticQuantityConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImlementation) => recordBuilder.WithBackwardsImplementation(forwardsImlementation);

    private static void RecordForwardsBehaviour(IQuantityConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour, ExpressionSyntax syntax) => recordBuilder.WithForwardsBehaviour(forwardsBehaviour, syntax);
    private static void RecordForwardsBehaviour(ISemanticQuantityConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour) => recordBuilder.WithForwardsBehaviour(forwardsBehaviour);

    private static void RecordForwardsPropertyName(IQuantityConversionRecordBuilder recordBuilder, string? forwardsPropertyName, ExpressionSyntax syntax) => recordBuilder.WithForwardsPropertyName(forwardsPropertyName, syntax);
    private static void RecordForwardsPropertyName(ISemanticQuantityConversionRecordBuilder recordBuilder, string? forwardsPropertyName) => recordBuilder.WithForwardsPropertyName(forwardsPropertyName);

    private static void RecordForwardsMethodName(IQuantityConversionRecordBuilder recordBuilder, string? forwardsMethodName, ExpressionSyntax syntax) => recordBuilder.WithForwardsMethodName(forwardsMethodName, syntax);
    private static void RecordForwardsMethodName(ISemanticQuantityConversionRecordBuilder recordBuilder, string? forwardsMethodName) => recordBuilder.WithForwardsMethodName(forwardsMethodName);

    private static void RecordForwardsStaticMethodName(IQuantityConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName, ExpressionSyntax syntax) => recordBuilder.WithForwardsStaticMethodName(forwardsStaticMethodName, syntax);
    private static void RecordForwardsStaticMethodName(ISemanticQuantityConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName) => recordBuilder.WithForwardsStaticMethodName(forwardsStaticMethodName);

    private static void RecordBackwardsImplementation(IQuantityConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImplementation, ExpressionSyntax syntax) => recordBuilder.WithBackwardsImplementation(forwardsImplementation, syntax);
    private static void RecordBackwardsImplementation(ISemanticQuantityConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImlementation) => recordBuilder.WithBackwardsImplementation(forwardsImlementation);

    private static void RecordBackwardsBehaviour(IQuantityConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour, ExpressionSyntax syntax) => recordBuilder.WithBackwardsBehaviour(forwardsBehaviour, syntax);
    private static void RecordBackwardsBehaviour(ISemanticQuantityConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour) => recordBuilder.WithBackwardsBehaviour(forwardsBehaviour);

    private static void RecordBackwardsStaticMethodName(IQuantityConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName, ExpressionSyntax syntax) => recordBuilder.WithBackwardsStaticMethodName(forwardsStaticMethodName, syntax);
    private static void RecordBackwardsStaticMethodName(ISemanticQuantityConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName) => recordBuilder.WithBackwardsStaticMethodName(forwardsStaticMethodName);
}
