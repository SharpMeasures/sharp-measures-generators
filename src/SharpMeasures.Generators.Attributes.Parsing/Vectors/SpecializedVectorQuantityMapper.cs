namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class SpecializedVectorQuantityMapper : AAdaptiveMapper<ISpecializedVectorQuantityRecordBuilder, ISemanticSpecializedVectorQuantityRecordBuilder>
{
    /// <summary>Instantiates a <see cref="SpecializedVectorQuantityMapper"/>, mapping the parameters of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public SpecializedVectorQuantityMapper(IAdaptiveMapperDependencyProvider<ISpecializedVectorQuantityRecordBuilder, ISemanticSpecializedVectorQuantityRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<ISpecializedVectorQuantityRecordBuilder, ISemanticSpecializedVectorQuantityRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordOriginal, RecordOriginal));
    }

    private static void RecordOriginal(ISpecializedVectorQuantityRecordBuilder recordBuilder, ITypeSymbol original, ExpressionSyntax syntax) => recordBuilder.WithOriginal(original, syntax);
    private static void RecordOriginal(ISemanticSpecializedVectorQuantityRecordBuilder recordBuilder, ITypeSymbol original) => recordBuilder.WithOriginal(original);
}
