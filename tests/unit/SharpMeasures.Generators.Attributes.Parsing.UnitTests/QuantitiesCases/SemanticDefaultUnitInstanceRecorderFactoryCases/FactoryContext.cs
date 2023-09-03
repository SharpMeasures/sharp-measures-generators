namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.SemanticDefaultUnitInstanceRecorderFactoryCases;

using Moq;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<ISemanticRecorderFactory> innerFactoryMock = new();
        Mock<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>> mapperMock = new();

        SemanticDefaultUnitInstanceRecorderFactory factory = new(innerFactoryMock.Object, mapperMock.Object);

        return new(factory, innerFactoryMock, mapperMock);
    }

    public SemanticDefaultUnitInstanceRecorderFactory Factory { get; }

    public Mock<ISemanticRecorderFactory> InnerFactoryMock { get; }
    public Mock<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>> MapperMock { get; }

    private FactoryContext(SemanticDefaultUnitInstanceRecorderFactory factory, Mock<ISemanticRecorderFactory> innerFactoryMock, Mock<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>> mapperMock)
    {
        Factory = factory;

        InnerFactoryMock = innerFactoryMock;
        MapperMock = mapperMock;
    }
}
