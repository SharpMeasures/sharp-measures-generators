namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class SpecializedVectorGroupMapper : AAdaptiveMapper<ISpecializedVectorGroupRecordBuilder, ISemanticSpecializedVectorGroupRecordBuilder>
{
    /// <summary>Instantiates a <see cref="SpecializedVectorGroupMapper"/>, mapping the parameters of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public SpecializedVectorGroupMapper(IAdaptiveMapperDependencyProvider<ISpecializedVectorGroupRecordBuilder, ISemanticSpecializedVectorGroupRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<ISpecializedVectorGroupRecordBuilder, ISemanticSpecializedVectorGroupRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordOriginal, RecordOriginal));
    }

    private static void RecordOriginal(ISpecializedVectorGroupRecordBuilder recordBuilder, ITypeSymbol original, ExpressionSyntax syntax) => recordBuilder.WithOriginal(original, syntax);
    private static void RecordOriginal(ISemanticSpecializedVectorGroupRecordBuilder recordBuilder, ITypeSymbol original) => recordBuilder.WithOriginal(original);
}
