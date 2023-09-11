namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityDifferenceMapperCases;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class MapperContext
{
    public static MapperContext Create(IAdaptiveMapperDependencyProvider<IQuantityDifferenceRecordBuilder, ISemanticQuantityDifferenceRecordBuilder> dependencyProvider) => new(new QuantityDifferenceMapper(dependencyProvider));

    public QuantityDifferenceMapper Mapper { get; }

    private MapperContext(QuantityDifferenceMapper mapper)
    {
        Mapper = mapper;
    }
}
