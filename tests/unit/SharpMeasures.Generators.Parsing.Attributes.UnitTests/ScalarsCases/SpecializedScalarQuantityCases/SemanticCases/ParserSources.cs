namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.SpecializedScalarQuantityCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticSpecializedScalarQuantityParser>
{
    protected override IEnumerable<ISemanticSpecializedScalarQuantityParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticSpecializedScalarQuantityParser>()
    };
}
