namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.SemanticDefaultUnitInstanceRecorderFactoryCases;

using Moq;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;

using Xunit;

public sealed class Create
{
    private static ISemanticRecorder<ISemanticDefaultUnitInstanceRecord> Target(ISemanticDefaultUnitInstanceRecorderFactory factory) => factory.Create();

    private FactoryContext Context { get; } = FactoryContext.Create();

    [Fact]
    public void Valid_UsesInnerFactory()
    {
        Mock<ISemanticRecorder<ISemanticDefaultUnitInstanceRecord>> recorderMock = new();

        Context.InnerFactoryMock.Setup(static (factory) => factory.Create<ISemanticDefaultUnitInstanceRecord, ISemanticDefaultUnitInstanceRecordBuilder>(It.IsAny<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>>(), It.IsAny<ISemanticDefaultUnitInstanceRecordBuilder>())).Returns(recorderMock.Object);

        var actual = Target(Context.Factory);

        Assert.Equal(recorderMock.Object, actual);

        Context.InnerFactoryMock.Verify((factory) => factory.Create<ISemanticDefaultUnitInstanceRecord, ISemanticDefaultUnitInstanceRecordBuilder>(Context.MapperMock.Object, It.IsAny<ISemanticDefaultUnitInstanceRecordBuilder>()), Times.Once);
    }
}
