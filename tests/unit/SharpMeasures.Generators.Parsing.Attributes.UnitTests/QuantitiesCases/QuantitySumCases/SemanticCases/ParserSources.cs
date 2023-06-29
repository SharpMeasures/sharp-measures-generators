namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantitySumCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticQuantitySumParser>
{
    protected override IEnumerable<ISemanticQuantitySumParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticQuantitySumParser>()
    };
}
