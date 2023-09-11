namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;

internal sealed class RecordBuilderContext
{
    public static RecordBuilderContext Create()
    {
        Mock<ICombinedRecorderFactory> innerFactoryMock = new();

        IDefaultUnitInstanceRecordBuilder recordBuilder = null!;
        var attributeSyntax = AttributeSyntaxFactory.Create();

        innerFactoryMock.Setup(static (factory) => factory.Create<IDefaultUnitInstanceRecord, IDefaultUnitInstanceRecordBuilder>(It.IsAny<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>>(), It.IsAny<IDefaultUnitInstanceRecordBuilder>())).Callback<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>, IDefaultUnitInstanceRecordBuilder>((_, _recordBuilder) => recordBuilder = _recordBuilder);

        DefaultUnitInstanceRecorderFactory factory = new(innerFactoryMock.Object, Mock.Of<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>>());

        ((IDefaultUnitInstanceRecorderFactory)factory).Create(attributeSyntax);

        return new(recordBuilder, attributeSyntax);
    }

    public IDefaultUnitInstanceRecordBuilder RecordBuilder { get; }

    public AttributeSyntax AttributeSyntax { get; }

    private RecordBuilderContext(IDefaultUnitInstanceRecordBuilder recordBuilder, AttributeSyntax attributeSyntax)
    {
        RecordBuilder = recordBuilder;
        AttributeSyntax = attributeSyntax;
    }
}
