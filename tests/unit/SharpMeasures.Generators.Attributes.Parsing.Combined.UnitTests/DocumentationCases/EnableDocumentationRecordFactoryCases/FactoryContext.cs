namespace SharpMeasures.Generators.Attributes.Parsing.DocumentationCases.DisableDocumentationRecordFactoryCases;

using SharpMeasures.Generators.Attributes.Parsing.Documentation;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        DisableDocumentationRecordFactory factory = new();

        return new(factory);
    }

    public DisableDocumentationRecordFactory Factory { get; }

    private FactoryContext(DisableDocumentationRecordFactory factory)
    {
        Factory = factory;
    }
}
