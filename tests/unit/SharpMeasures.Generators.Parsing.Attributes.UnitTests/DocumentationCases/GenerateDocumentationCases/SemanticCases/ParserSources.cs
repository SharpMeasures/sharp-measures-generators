namespace SharpMeasures.Generators.Parsing.Attributes.DocumentationCases.GenerateDocumentationCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Documentation;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticGenerateDocumentationParser>
{
    protected override IEnumerable<ISemanticGenerateDocumentationParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticGenerateDocumentationParser>()
    };
}
