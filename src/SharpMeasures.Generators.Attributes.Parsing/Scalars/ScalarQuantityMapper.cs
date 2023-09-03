namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="ScalarQuantityAttribute{TUnit}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class ScalarQuantityMapper : AAdaptiveMapper<IScalarQuantityRecordBuilder, ISemanticScalarQuantityRecordBuilder>
{
    /// <summary>Instantiates a <see cref="ScalarQuantityMapper"/>, mapping the parameters of <see cref="ScalarQuantityAttribute{TUnit}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public ScalarQuantityMapper(IAdaptiveMapperDependencyProvider<IScalarQuantityRecordBuilder, ISemanticScalarQuantityRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IScalarQuantityRecordBuilder, ISemanticScalarQuantityRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordUnit, RecordUnit));

        repository.NamedParameters.AddNamedMapping(nameof(ScalarQuantityAttribute<object>.Biased), (factory) => factory.Create(BoolPattern, RecordBiased, RecordBiased));
    }

    private static IArgumentPattern<bool> BoolPattern(IArgumentPatternFactory factory) => factory.Bool();

    private static void RecordUnit(IScalarQuantityRecordBuilder recordBuilder, ITypeSymbol unit, ExpressionSyntax syntax) => recordBuilder.WithUnit(unit, syntax);
    private static void RecordUnit(ISemanticScalarQuantityRecordBuilder recordBuilder, ITypeSymbol unit) => recordBuilder.WithUnit(unit);

    private static void RecordBiased(IScalarQuantityRecordBuilder recordBuilder, bool biased, ExpressionSyntax syntax) => recordBuilder.WithBiased(biased, syntax);
    private static void RecordBiased(ISemanticScalarQuantityRecordBuilder recordBuilder, bool biased) => recordBuilder.WithBiased(biased);
}
