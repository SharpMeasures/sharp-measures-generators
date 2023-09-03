namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="VectorQuantityAttribute{TUnit}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class VectorQuantityMapper : AAdaptiveMapper<IVectorQuantityRecordBuilder, ISemanticVectorQuantityRecordBuilder>
{
    /// <summary>Instantiates a <see cref="VectorQuantityMapper"/>, mapping the parameters of <see cref="VectorQuantityAttribute{TUnit}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public VectorQuantityMapper(IAdaptiveMapperDependencyProvider<IVectorQuantityRecordBuilder, ISemanticVectorQuantityRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IVectorQuantityRecordBuilder, ISemanticVectorQuantityRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordUnit, RecordUnit));

        repository.NamedParameters.AddNamedMapping(nameof(VectorQuantityAttribute<object>.Dimension), (factory) => factory.Create(IntPattern, RecordDimension, RecordDimension));
    }

    private static IArgumentPattern<int> IntPattern(IArgumentPatternFactory factory) => factory.Int();

    private static void RecordUnit(IVectorQuantityRecordBuilder recordBuilder, ITypeSymbol unit, ExpressionSyntax syntax) => recordBuilder.WithUnit(unit, syntax);
    private static void RecordUnit(ISemanticVectorQuantityRecordBuilder recordBuilder, ITypeSymbol unit) => recordBuilder.WithUnit(unit);

    private static void RecordDimension(IVectorQuantityRecordBuilder recordBuilder, int dimension, ExpressionSyntax syntax) => recordBuilder.WithDimension(dimension, syntax);
    private static void RecordDimension(ISemanticVectorQuantityRecordBuilder recordBuilder, int dimension) => recordBuilder.WithDimension(dimension);
}
