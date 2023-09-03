namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;

using System;

using Xunit;

public sealed class Create
{
    private static ICombinedRecorder<IDefaultUnitInstanceRecord> Target(IDefaultUnitInstanceRecorderFactory factory, AttributeSyntax attributeSyntax) => factory.Create(attributeSyntax);

    private FactoryContext Context { get; } = FactoryContext.Create();

    [Fact]
    public void NullAttributeSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidAttributeSyntax_UsesInnerFactory()
    {
        Mock<ICombinedRecorder<IDefaultUnitInstanceRecord>> recorderMock = new();

        Context.InnerFactoryMock.Setup(static (factory) => factory.Create<IDefaultUnitInstanceRecord, IDefaultUnitInstanceRecordBuilder>(It.IsAny<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>>(), It.IsAny<IDefaultUnitInstanceRecordBuilder>())).Returns(recorderMock.Object);

        var actual = Target(Context.Factory, AttributeSyntaxFactory.Create());

        Assert.Equal(recorderMock.Object, actual);

        Context.InnerFactoryMock.Verify((factory) => factory.Create<IDefaultUnitInstanceRecord, IDefaultUnitInstanceRecordBuilder>(Context.MapperMock.Object, It.IsAny<IDefaultUnitInstanceRecordBuilder>()), Times.Once);
    }
}
