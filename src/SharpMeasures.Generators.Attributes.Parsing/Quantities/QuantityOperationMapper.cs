namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="QuantityOperationAttribute{TResult, TOther}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class QuantityOperationMapper : AAdaptiveMapper<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder>
{
    /// <summary>Instantiates a <see cref="QuantityOperationMapper"/>, mapping the parameters of <see cref="QuantityOperationAttribute{TResult, TOther}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public QuantityOperationMapper(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordResult, RecordResult));
        repository.TypeParameters.AddIndexedMapping(1, (factory) => factory.Create(RecordOther, RecordOther));

        repository.ConstructorParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.OperatorType), (factory) => factory.Normal.Create(OperatorTypePattern, RecordOperatorType, RecordOperatorType));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.Position), (factory) => factory.Create(OperationPositionPattern, RecordPosition, RecordPosition));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.MirrorMode), (factory) => factory.Create(OperationMirrorModePattern, RecordMirrorMode, RecordMirrorMode));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.Implementation), (factory) => factory.Create(OperationImplementationPattern, RecordImplementation, RecordImplementation));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.MirroredImplementation), (factory) => factory.Create(OperationImplementationPattern, RecordMirroredImplementation, RecordMirroredImplementation));

        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.MethodName), (factory) => factory.Create(NullableStringPattern, RecordMethodName, RecordMethodName));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.StaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordStaticMethodName, RecordStaticMethodName));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.MirroredMethodName), (factory) => factory.Create(NullableStringPattern, RecordMirroredMethodName, RecordMirroredMethodName));
        repository.NamedParameters.AddNamedMapping(nameof(QuantityOperationAttribute<object, object>.MirroredStaticMethodName), (factory) => factory.Create(NullableStringPattern, RecordMirroredStaticMethodName, RecordMirroredStaticMethodName));
    }

    private static IArgumentPattern<OperatorType> OperatorTypePattern(IArgumentPatternFactory factory) => factory.Enum<OperatorType>();
    private static IArgumentPattern<OperationPosition> OperationPositionPattern(IArgumentPatternFactory factory) => factory.Enum<OperationPosition>();
    private static IArgumentPattern<OperationMirrorMode> OperationMirrorModePattern(IArgumentPatternFactory factory) => factory.Enum<OperationMirrorMode>();
    private static IArgumentPattern<OperationImplementation> OperationImplementationPattern(IArgumentPatternFactory factory) => factory.Enum<OperationImplementation>();
    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordResult(IQuantityOperationRecordBuilder recordBuilder, ITypeSymbol result, ExpressionSyntax syntax) => recordBuilder.WithResult(result, syntax);
    private static void RecordResult(ISemanticQuantityOperationRecordBuilder recordBuilder, ITypeSymbol result) => recordBuilder.WithResult(result);

    private static void RecordOther(IQuantityOperationRecordBuilder recordBuilder, ITypeSymbol other, ExpressionSyntax syntax) => recordBuilder.WithOther(other, syntax);
    private static void RecordOther(ISemanticQuantityOperationRecordBuilder recordBuilder, ITypeSymbol other) => recordBuilder.WithOther(other);

    private static void RecordOperatorType(IQuantityOperationRecordBuilder recordBuilder, OperatorType operatorType, ExpressionSyntax syntax) => recordBuilder.WithOperatorType(operatorType, syntax);
    private static void RecordOperatorType(ISemanticQuantityOperationRecordBuilder recordBuilder, OperatorType operatorType) => recordBuilder.WithOperatorType(operatorType);

    private static void RecordPosition(IQuantityOperationRecordBuilder recordBuilder, OperationPosition position, ExpressionSyntax syntax) => recordBuilder.WithPosition(position, syntax);
    private static void RecordPosition(ISemanticQuantityOperationRecordBuilder recordBuilder, OperationPosition position) => recordBuilder.WithPosition(position);

    private static void RecordMirrorMode(IQuantityOperationRecordBuilder recordBuilder, OperationMirrorMode mirrorMode, ExpressionSyntax syntax) => recordBuilder.WithMirrorMode(mirrorMode, syntax);
    private static void RecordMirrorMode(ISemanticQuantityOperationRecordBuilder recordBuilder, OperationMirrorMode mirrorMode) => recordBuilder.WithMirrorMode(mirrorMode);

    private static void RecordImplementation(IQuantityOperationRecordBuilder recordBuilder, OperationImplementation implementation, ExpressionSyntax syntax) => recordBuilder.WithImplementation(implementation, syntax);
    private static void RecordImplementation(ISemanticQuantityOperationRecordBuilder recordBuilder, OperationImplementation implementation) => recordBuilder.WithImplementation(implementation);

    private static void RecordMirroredImplementation(IQuantityOperationRecordBuilder recordBuilder, OperationImplementation mirroredImplementation, ExpressionSyntax syntax) => recordBuilder.WithMirroredImplementation(mirroredImplementation, syntax);
    private static void RecordMirroredImplementation(ISemanticQuantityOperationRecordBuilder recordBuilder, OperationImplementation mirroredImplementation) => recordBuilder.WithMirroredImplementation(mirroredImplementation);

    private static void RecordMethodName(IQuantityOperationRecordBuilder recordBuilder, string? methodName, ExpressionSyntax syntax) => recordBuilder.WithMethodName(methodName, syntax);
    private static void RecordMethodName(ISemanticQuantityOperationRecordBuilder recordBuilder, string? methodName) => recordBuilder.WithMethodName(methodName);

    private static void RecordStaticMethodName(IQuantityOperationRecordBuilder recordBuilder, string? staticMethodName, ExpressionSyntax syntax) => recordBuilder.WithStaticMethodName(staticMethodName, syntax);
    private static void RecordStaticMethodName(ISemanticQuantityOperationRecordBuilder recordBuilder, string? staticMethodName) => recordBuilder.WithStaticMethodName(staticMethodName);

    private static void RecordMirroredMethodName(IQuantityOperationRecordBuilder recordBuilder, string? mirroredMethodName, ExpressionSyntax syntax) => recordBuilder.WithMirroredMethodName(mirroredMethodName, syntax);
    private static void RecordMirroredMethodName(ISemanticQuantityOperationRecordBuilder recordBuilder, string? mirroredMethodName) => recordBuilder.WithMirroredMethodName(mirroredMethodName);

    private static void RecordMirroredStaticMethodName(IQuantityOperationRecordBuilder recordBuilder, string? mirroredStaticMethodName, ExpressionSyntax syntax) => recordBuilder.WithMirroredStaticMethodName(mirroredStaticMethodName, syntax);
    private static void RecordMirroredStaticMethodName(ISemanticQuantityOperationRecordBuilder recordBuilder, string? mirroredStaticMethodName) => recordBuilder.WithMirroredStaticMethodName(mirroredStaticMethodName);
}
