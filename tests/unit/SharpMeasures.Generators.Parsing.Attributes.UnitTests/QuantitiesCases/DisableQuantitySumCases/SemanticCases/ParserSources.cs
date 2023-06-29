namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantitySumCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticDisableQuantitySumParser>
{
    protected override IEnumerable<ISemanticDisableQuantitySumParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticDisableQuantitySumParser>()
    };
}
