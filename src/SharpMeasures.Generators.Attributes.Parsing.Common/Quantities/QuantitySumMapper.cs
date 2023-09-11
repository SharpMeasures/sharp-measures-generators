namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="QuantitySumAttribute{TSum}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class QuantitySumMapper : AAdaptiveMapper<IQuantitySumRecordBuilder, ISemanticQuantitySumRecordBuilder>
{
    /// <summary>Instantiates a <see cref="QuantitySumMapper"/>, mapping the parameters of <see cref="QuantitySumAttribute{TSum}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public QuantitySumMapper(IAdaptiveMapperDependencyProvider<IQuantitySumRecordBuilder, ISemanticQuantitySumRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IQuantitySumRecordBuilder, ISemanticQuantitySumRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordSum, RecordSum));
    }

    private static void RecordSum(IQuantitySumRecordBuilder recordBuilder, ITypeSymbol sum, ExpressionSyntax syntax) => recordBuilder.WithSum(sum, syntax);
    private static void RecordSum(ISemanticQuantitySumRecordBuilder recordBuilder, ITypeSymbol sum) => recordBuilder.WithSum(sum);
}
