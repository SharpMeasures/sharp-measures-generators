namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityOperationMapperCases;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class MapperContext
{
    public static MapperContext Create(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider) => new(new QuantityOperationMapper(dependencyProvider));

    public QuantityOperationMapper Mapper { get; }

    private MapperContext(QuantityOperationMapper mapper)
    {
        Mapper = mapper;
    }
}
