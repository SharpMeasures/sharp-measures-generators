namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class SpecializedUnitlessQuantityMapper : AAdaptiveMapper<ISpecializedUnitlessQuantityRecordBuilder, ISemanticSpecializedUnitlessQuantityRecordBuilder>
{
    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantityMapper"/>, mapping the parameters of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public SpecializedUnitlessQuantityMapper(IAdaptiveMapperDependencyProvider<ISpecializedUnitlessQuantityRecordBuilder, ISemanticSpecializedUnitlessQuantityRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<ISpecializedUnitlessQuantityRecordBuilder, ISemanticSpecializedUnitlessQuantityRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordOriginal, RecordOriginal));
    }

    private static void RecordOriginal(ISpecializedUnitlessQuantityRecordBuilder recordBuilder, ITypeSymbol original, ExpressionSyntax syntax) => recordBuilder.WithOriginal(original, syntax);
    private static void RecordOriginal(ISemanticSpecializedUnitlessQuantityRecordBuilder recordBuilder, ITypeSymbol original) => recordBuilder.WithOriginal(original);
}
