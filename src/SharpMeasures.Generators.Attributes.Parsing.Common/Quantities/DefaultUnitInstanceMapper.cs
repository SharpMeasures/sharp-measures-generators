namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="DefaultUnitInstanceAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class DefaultUnitInstanceMapper : AAdaptiveMapper<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder>
{
    /// <summary>Instantiates a <see cref="DefaultUnitInstanceMapper"/>, mapping the parameters of <see cref="DefaultUnitInstanceAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public DefaultUnitInstanceMapper(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> repository)
    {
        repository.ConstructorParameters.AddNamedMapping(nameof(DefaultUnitInstanceAttribute.UnitInstance), (factory) => factory.Normal.Create(NullableStringPattern, RecordUnitInstance, RecordUnitInstance));
        repository.NamedParameters.AddNamedMapping(nameof(DefaultUnitInstanceAttribute.Symbol), (factory) => factory.Create(NullableStringPattern, RecordSymbol, RecordSymbol));
    }

    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordUnitInstance(IDefaultUnitInstanceRecordBuilder recordBuilder, string? unitInstance, ExpressionSyntax syntax) => recordBuilder.WithUnitInstance(unitInstance, syntax);
    private static void RecordUnitInstance(ISemanticDefaultUnitInstanceRecordBuilder recordBuilder, string? unitInstance) => recordBuilder.WithUnitInstance(unitInstance);

    private static void RecordSymbol(IDefaultUnitInstanceRecordBuilder recordBuilder, string? symbol, ExpressionSyntax syntax) => recordBuilder.WithSymbol(symbol, syntax);
    private static void RecordSymbol(ISemanticDefaultUnitInstanceRecordBuilder recordBuilder, string? symbol) => recordBuilder.WithSymbol(symbol);
}
