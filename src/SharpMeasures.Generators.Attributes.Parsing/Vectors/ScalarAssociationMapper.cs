namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="ScalarAssociationAttribute{TScalar}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class ScalarAssociationMapper : AAdaptiveMapper<IScalarAssociationRecordBuilder, ISemanticScalarAssociationRecordBuilder>
{
    /// <summary>Instantiates a <see cref="ScalarAssociationMapper"/>, mapping the parameters of <see cref="ScalarAssociationAttribute{TScalar}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public ScalarAssociationMapper(IAdaptiveMapperDependencyProvider<IScalarAssociationRecordBuilder, ISemanticScalarAssociationRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IScalarAssociationRecordBuilder, ISemanticScalarAssociationRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordScalarQuantity, RecordScalarQuantity));

        repository.NamedParameters.AddNamedMapping(nameof(ScalarAssociationAttribute<object>.AsComponents), (factory) => factory.Create(BoolPattern, RecordAsComponents, RecordAsComponents));
        repository.NamedParameters.AddNamedMapping(nameof(ScalarAssociationAttribute<object>.AsMagnitude), (factory) => factory.Create(BoolPattern, RecordAsMagnitude, RecordAsMagnitude));
    }

    private static IArgumentPattern<bool> BoolPattern(IArgumentPatternFactory factory) => factory.Bool();

    private static void RecordScalarQuantity(IScalarAssociationRecordBuilder recordBuilder, ITypeSymbol scalarQuantity, ExpressionSyntax syntax) => recordBuilder.WithScalarQuantity(scalarQuantity, syntax);
    private static void RecordScalarQuantity(ISemanticScalarAssociationRecordBuilder recordBuilder, ITypeSymbol scalarQuantity) => recordBuilder.WithScalarQuantity(scalarQuantity);

    private static void RecordAsComponents(IScalarAssociationRecordBuilder recordBuilder, bool asComponents, ExpressionSyntax syntax) => recordBuilder.WithAsComponents(asComponents, syntax);
    private static void RecordAsComponents(ISemanticScalarAssociationRecordBuilder recordBuilder, bool asComponents) => recordBuilder.WithAsComponents(asComponents);

    private static void RecordAsMagnitude(IScalarAssociationRecordBuilder recordBuilder, bool asMagnitude, ExpressionSyntax syntax) => recordBuilder.WithAsMagnitude(asMagnitude, syntax);
    private static void RecordAsMagnitude(ISemanticScalarAssociationRecordBuilder recordBuilder, bool asMagnitude) => recordBuilder.WithAsMagnitude(asMagnitude);
}
