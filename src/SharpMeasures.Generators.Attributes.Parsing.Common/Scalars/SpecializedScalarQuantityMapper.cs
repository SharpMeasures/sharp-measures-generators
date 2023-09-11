namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class SpecializedScalarQuantityMapper : AAdaptiveMapper<ISpecializedScalarQuantityRecordBuilder, ISemanticSpecializedScalarQuantityRecordBuilder>
{
    /// <summary>Instantiates a <see cref="SpecializedScalarQuantityMapper"/>, mapping the parameters of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public SpecializedScalarQuantityMapper(IAdaptiveMapperDependencyProvider<ISpecializedScalarQuantityRecordBuilder, ISemanticSpecializedScalarQuantityRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<ISpecializedScalarQuantityRecordBuilder, ISemanticSpecializedScalarQuantityRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordOriginal, RecordOriginal));
    }

    private static void RecordOriginal(ISpecializedScalarQuantityRecordBuilder recordBuilder, ITypeSymbol original, ExpressionSyntax syntax) => recordBuilder.WithOriginal(original, syntax);
    private static void RecordOriginal(ISemanticSpecializedScalarQuantityRecordBuilder recordBuilder, ITypeSymbol original) => recordBuilder.WithOriginal(original);
}
