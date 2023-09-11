namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

using System.Collections.Generic;

/// <summary>Maps the parameters of <see cref="TypeConversionAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class TypeConversionMapper : AAdaptiveMapper<ITypeConversionRecordBuilder, ISemanticTypeConversionRecordBuilder>
{
    /// <summary>Instantiates a <see cref="TypeConversionMapper"/>, mapping the parameters of <see cref="TypeConversionAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public TypeConversionMapper(IAdaptiveMapperDependencyProvider<ITypeConversionRecordBuilder, ISemanticTypeConversionRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<ITypeConversionRecordBuilder, ISemanticTypeConversionRecordBuilder> repository)
    {
        repository.ConstructorParameters.AddNamedMapping(nameof(TypeConversionAttribute.Types), (factory) => factory.Normal.Create(TypeArrayPattern, RecordTypes, RecordTypes));

        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.ForwardsImplementation), (factory) => factory.Create(ConversionImplementationPattern, RecordForwardsImplementation, RecordForwardsImplementation));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.ForwardsBehaviour), (factory) => factory.Create(ConversionOperatorBehaviourPattern, RecordForwardsBehaviour, RecordForwardsBehaviour));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.ForwardsPropertyName), (factory) => factory.Create(NullableStringPattern, RecordForwardsPropertyName, RecordForwardsPropertyName));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.ForwardsMethodName), (factory) => factory.Create(NullableStringPattern, RecordForwardsMethodName, RecordForwardsMethodName));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.ForwardsStaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordForwardsStaticMethodName, RecordForwardsStaticMethodName));

        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.BackwardsImplementation), (factory) => factory.Create(ConversionImplementationPattern, RecordBackwardsImplementation, RecordBackwardsImplementation));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.BackwardsBehaviour), (factory) => factory.Create(ConversionOperatorBehaviourPattern, RecordBackwardsBehaviour, RecordBackwardsBehaviour));
        repository.NamedParameters.AddNamedMapping(nameof(TypeConversionAttribute.BackwardsStaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordBackwardsStaticMethodName, RecordBackwardsStaticMethodName));
    }

    private static IArgumentPattern<ITypeSymbol?[]?> TypeArrayPattern(IArgumentPatternFactory factory) => factory.NullableArray(factory.NullableType());
    private static IArgumentPattern<ConversionImplementation> ConversionImplementationPattern(IArgumentPatternFactory factory) => factory.Enum<ConversionImplementation>();
    private static IArgumentPattern<ConversionOperatorBehaviour> ConversionOperatorBehaviourPattern(IArgumentPatternFactory factory) => factory.Enum<ConversionOperatorBehaviour>();
    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordTypes(ITypeConversionRecordBuilder recordBuilder, IReadOnlyList<ITypeSymbol?>? types, ExpressionSyntax syntax) => recordBuilder.WithTypes(types, syntax);
    private static void RecordTypes(ISemanticTypeConversionRecordBuilder recordBuilder, IReadOnlyList<ITypeSymbol?>? types) => recordBuilder.WithTypes(types);

    private static void RecordForwardsImplementation(ITypeConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImplementation, ExpressionSyntax syntax) => recordBuilder.WithForwardsImplementation(forwardsImplementation, syntax);
    private static void RecordForwardsImplementation(ISemanticTypeConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImlementation) => recordBuilder.WithBackwardsImplementation(forwardsImlementation);

    private static void RecordForwardsBehaviour(ITypeConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour, ExpressionSyntax syntax) => recordBuilder.WithForwardsBehaviour(forwardsBehaviour, syntax);
    private static void RecordForwardsBehaviour(ISemanticTypeConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour) => recordBuilder.WithForwardsBehaviour(forwardsBehaviour);

    private static void RecordForwardsPropertyName(ITypeConversionRecordBuilder recordBuilder, string? forwardsPropertyName, ExpressionSyntax syntax) => recordBuilder.WithForwardsPropertyName(forwardsPropertyName, syntax);
    private static void RecordForwardsPropertyName(ISemanticTypeConversionRecordBuilder recordBuilder, string? forwardsPropertyName) => recordBuilder.WithForwardsPropertyName(forwardsPropertyName);

    private static void RecordForwardsMethodName(ITypeConversionRecordBuilder recordBuilder, string? forwardsMethodName, ExpressionSyntax syntax) => recordBuilder.WithForwardsMethodName(forwardsMethodName, syntax);
    private static void RecordForwardsMethodName(ISemanticTypeConversionRecordBuilder recordBuilder, string? forwardsMethodName) => recordBuilder.WithForwardsMethodName(forwardsMethodName);

    private static void RecordForwardsStaticMethodName(ITypeConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName, ExpressionSyntax syntax) => recordBuilder.WithForwardsStaticMethodName(forwardsStaticMethodName, syntax);
    private static void RecordForwardsStaticMethodName(ISemanticTypeConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName) => recordBuilder.WithForwardsStaticMethodName(forwardsStaticMethodName);

    private static void RecordBackwardsImplementation(ITypeConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImplementation, ExpressionSyntax syntax) => recordBuilder.WithBackwardsImplementation(forwardsImplementation, syntax);
    private static void RecordBackwardsImplementation(ISemanticTypeConversionRecordBuilder recordBuilder, ConversionImplementation forwardsImlementation) => recordBuilder.WithBackwardsImplementation(forwardsImlementation);

    private static void RecordBackwardsBehaviour(ITypeConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour, ExpressionSyntax syntax) => recordBuilder.WithBackwardsBehaviour(forwardsBehaviour, syntax);
    private static void RecordBackwardsBehaviour(ISemanticTypeConversionRecordBuilder recordBuilder, ConversionOperatorBehaviour forwardsBehaviour) => recordBuilder.WithBackwardsBehaviour(forwardsBehaviour);

    private static void RecordBackwardsStaticMethodName(ITypeConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName, ExpressionSyntax syntax) => recordBuilder.WithBackwardsStaticMethodName(forwardsStaticMethodName, syntax);
    private static void RecordBackwardsStaticMethodName(ISemanticTypeConversionRecordBuilder recordBuilder, string? forwardsStaticMethodName) => recordBuilder.WithBackwardsStaticMethodName(forwardsStaticMethodName);
}
