namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="VectorGroupMemberAttribute{TGroup}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class VectorGroupMemberMapper : AAdaptiveMapper<IVectorGroupMemberRecordBuilder, ISemanticVectorGroupMemberRecordBuilder>
{
    /// <summary>Instantiates a <see cref="VectorGroupMemberMapper"/>, mapping the parameters of <see cref="VectorGroupMemberAttribute{TGroup}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public VectorGroupMemberMapper(IAdaptiveMapperDependencyProvider<IVectorGroupMemberRecordBuilder, ISemanticVectorGroupMemberRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IVectorGroupMemberRecordBuilder, ISemanticVectorGroupMemberRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordGroup, RecordGroup));

        repository.NamedParameters.AddNamedMapping(nameof(VectorGroupMemberAttribute<object>.Dimension), (factory) => factory.Create(IntPattern, RecordDimension, RecordDimension));
    }

    private static IArgumentPattern<int> IntPattern(IArgumentPatternFactory factory) => factory.Int();

    private static void RecordGroup(IVectorGroupMemberRecordBuilder recordBuilder, ITypeSymbol group, ExpressionSyntax syntax) => recordBuilder.WithGroup(group, syntax);
    private static void RecordGroup(ISemanticVectorGroupMemberRecordBuilder recordBuilder, ITypeSymbol group) => recordBuilder.WithGroup(group);

    private static void RecordDimension(IVectorGroupMemberRecordBuilder recordBuilder, int dimension, ExpressionSyntax syntax) => recordBuilder.WithDimension(dimension, syntax);
    private static void RecordDimension(ISemanticVectorGroupMemberRecordBuilder recordBuilder, int dimension) => recordBuilder.WithDimension(dimension);
}
