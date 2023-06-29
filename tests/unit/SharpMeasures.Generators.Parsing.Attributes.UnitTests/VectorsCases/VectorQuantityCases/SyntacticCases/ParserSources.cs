namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorQuantityCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticVectorQuantityParser>
{
    protected override IEnumerable<ISyntacticVectorQuantityParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticVectorQuantityParser>()
    };
}
