namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="VectorAssociationAttribute{TVector}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class VectorAssociationMapper : AAdaptiveMapper<IVectorAssociationRecordBuilder, ISemanticVectorAssociationRecordBuilder>
{
    /// <summary>Instantiates a <see cref="VectorAssociationMapper"/>, mapping the parameters of <see cref="VectorAssociationAttribute{TVector}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public VectorAssociationMapper(IAdaptiveMapperDependencyProvider<IVectorAssociationRecordBuilder, ISemanticVectorAssociationRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IVectorAssociationRecordBuilder, ISemanticVectorAssociationRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordVectorQuantity, RecordVectorQuantity));
    }

    private static void RecordVectorQuantity(IVectorAssociationRecordBuilder recordBuilder, ITypeSymbol vectorQuantity, ExpressionSyntax syntax) => recordBuilder.WithVectorQuantity(vectorQuantity, syntax);
    private static void RecordVectorQuantity(ISemanticVectorAssociationRecordBuilder recordBuilder, ITypeSymbol vectorQuantity) => recordBuilder.WithVectorQuantity(vectorQuantity);
}
