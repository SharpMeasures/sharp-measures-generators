namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="UnitAttribute{TScalar}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class UnitMapper : AAdaptiveMapper<IUnitRecordBuilder, ISemanticUnitRecordBuilder>
{
    /// <summary>Instantiates a <see cref="UnitMapper"/>, mapping the parameters of <see cref="UnitAttribute{TScalar}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public UnitMapper(IAdaptiveMapperDependencyProvider<IUnitRecordBuilder, ISemanticUnitRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IUnitRecordBuilder, ISemanticUnitRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordScalarQuantity, RecordScalarQuantity));

        repository.NamedParameters.AddNamedMapping(nameof(UnitAttribute<object>.BiasTerm), (factory) => factory.Create(BoolPattern, RecordBiasTerm, RecordBiasTerm));
    }

    private static IArgumentPattern<bool> BoolPattern(IArgumentPatternFactory factory) => factory.Bool();

    private static void RecordScalarQuantity(IUnitRecordBuilder recordBuilder, ITypeSymbol scalarQuantity, ExpressionSyntax syntax) => recordBuilder.WithScalarQuantity(scalarQuantity, syntax);
    private static void RecordScalarQuantity(ISemanticUnitRecordBuilder recordBuilder, ITypeSymbol scalarQuantity) => recordBuilder.WithScalarQuantity(scalarQuantity);

    private static void RecordBiasTerm(IUnitRecordBuilder recordBuilder, bool biasTerm, ExpressionSyntax syntax) => recordBuilder.WithBiasTerm(biasTerm, syntax);
    private static void RecordBiasTerm(ISemanticUnitRecordBuilder recordBuilder, bool biasTerm) => recordBuilder.WithBiasTerm(biasTerm);
}
