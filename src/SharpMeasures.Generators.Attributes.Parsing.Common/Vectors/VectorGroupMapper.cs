namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="VectorGroupAttribute{TUnit}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class VectorGroupMapper : AAdaptiveMapper<IVectorGroupRecordBuilder, ISemanticVectorGroupRecordBuilder>
{
    /// <summary>Instantiates a <see cref="VectorGroupMapper"/>, mapping the parameters of <see cref="VectorGroupAttribute{TUnit}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public VectorGroupMapper(IAdaptiveMapperDependencyProvider<IVectorGroupRecordBuilder, ISemanticVectorGroupRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IVectorGroupRecordBuilder, ISemanticVectorGroupRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordUnit, RecordUnit));
    }

    private static void RecordUnit(IVectorGroupRecordBuilder recordBuilder, ITypeSymbol unit, ExpressionSyntax syntax) => recordBuilder.WithUnit(unit, syntax);
    private static void RecordUnit(ISemanticVectorGroupRecordBuilder recordBuilder, ITypeSymbol unit) => recordBuilder.WithUnit(unit);
}
