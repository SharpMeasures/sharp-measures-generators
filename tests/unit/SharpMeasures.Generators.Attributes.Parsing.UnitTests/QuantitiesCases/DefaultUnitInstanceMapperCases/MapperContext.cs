namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceMapperCases;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class MapperContext
{
    public static MapperContext Create(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider) => new(new DefaultUnitInstanceMapper(dependencyProvider));

    public DefaultUnitInstanceMapper Mapper { get; }

    private MapperContext(DefaultUnitInstanceMapper mapper)
    {
        Mapper = mapper;
    }
}
