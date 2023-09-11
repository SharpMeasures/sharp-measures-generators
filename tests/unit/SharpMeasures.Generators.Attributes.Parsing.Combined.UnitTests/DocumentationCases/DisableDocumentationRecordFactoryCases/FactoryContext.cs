namespace SharpMeasures.Generators.Attributes.Parsing.DocumentationCases.EnableDocumentationRecordFactoryCases;

using SharpMeasures.Generators.Attributes.Parsing.Documentation;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        EnableDocumentationRecordFactory factory = new();

        return new(factory);
    }

    public EnableDocumentationRecordFactory Factory { get; }

    private FactoryContext(EnableDocumentationRecordFactory factory)
    {
        Factory = factory;
    }
}
