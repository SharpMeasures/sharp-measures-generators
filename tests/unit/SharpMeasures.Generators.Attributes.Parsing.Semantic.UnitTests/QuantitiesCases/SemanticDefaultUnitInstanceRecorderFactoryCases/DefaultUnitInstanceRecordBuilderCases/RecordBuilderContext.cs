namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.SemanticDefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using Moq;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;

internal sealed class RecordBuilderContext
{
    public static RecordBuilderContext Create()
    {
        Mock<ISemanticRecorderFactory> innerFactoryMock = new();

        ISemanticDefaultUnitInstanceRecordBuilder recordBuilder = null!;

        innerFactoryMock.Setup(static (factory) => factory.Create<ISemanticDefaultUnitInstanceRecord, ISemanticDefaultUnitInstanceRecordBuilder>(It.IsAny<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>>(), It.IsAny<ISemanticDefaultUnitInstanceRecordBuilder>())).Callback<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>, ISemanticDefaultUnitInstanceRecordBuilder>((_, _recordBuilder) => recordBuilder = _recordBuilder);

        SemanticDefaultUnitInstanceRecorderFactory factory = new(innerFactoryMock.Object, Mock.Of<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>>());

        ((ISemanticDefaultUnitInstanceRecorderFactory)factory).Create();

        return new(recordBuilder);
    }

    public ISemanticDefaultUnitInstanceRecordBuilder RecordBuilder { get; }

    private RecordBuilderContext(ISemanticDefaultUnitInstanceRecordBuilder recordBuilder)
    {
        RecordBuilder = recordBuilder;
    }
}
