namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="QuantityDifferenceAttribute{TDifference}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class QuantityDifferenceMapper : AAdaptiveMapper<IQuantityDifferenceRecordBuilder, ISemanticQuantityDifferenceRecordBuilder>
{
    /// <summary>Instantiates a <see cref="QuantityDifferenceMapper"/>, mapping the parameters of <see cref="QuantityDifferenceAttribute{TDifference}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public QuantityDifferenceMapper(IAdaptiveMapperDependencyProvider<IQuantityDifferenceRecordBuilder, ISemanticQuantityDifferenceRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IQuantityDifferenceRecordBuilder, ISemanticQuantityDifferenceRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordDifference, RecordDifference));
    }

    private static void RecordDifference(IQuantityDifferenceRecordBuilder recordBuilder, ITypeSymbol difference, ExpressionSyntax syntax) => recordBuilder.WithDifference(difference, syntax);
    private static void RecordDifference(ISemanticQuantityDifferenceRecordBuilder recordBuilder, ITypeSymbol difference) => recordBuilder.WithDifference(difference);
}
