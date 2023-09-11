namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases;

using Moq;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<ICombinedRecorderFactory> innerFactoryMock = new();
        Mock<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>> mapperMock = new();

        DefaultUnitInstanceRecorderFactory factory = new(innerFactoryMock.Object, mapperMock.Object);

        return new(factory, innerFactoryMock, mapperMock);
    }

    public DefaultUnitInstanceRecorderFactory Factory { get; }

    public Mock<ICombinedRecorderFactory> InnerFactoryMock { get; }
    public Mock<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>> MapperMock { get; }

    private FactoryContext(DefaultUnitInstanceRecorderFactory factory, Mock<ICombinedRecorderFactory> innerFactoryMock, Mock<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>> mapperMock)
    {
        Factory = factory;

        InnerFactoryMock = innerFactoryMock;
        MapperMock = mapperMock;
    }
}
