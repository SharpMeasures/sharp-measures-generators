namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantitySumMapperCases;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class MapperContext
{
    public static MapperContext Create(IAdaptiveMapperDependencyProvider<IQuantitySumRecordBuilder, ISemanticQuantitySumRecordBuilder> dependencyProvider) => new(new QuantitySumMapper(dependencyProvider));

    public QuantitySumMapper Mapper { get; }

    private MapperContext(QuantitySumMapper mapper)
    {
        Mapper = mapper;
    }
}
