namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantitySumCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticDisableQuantitySumParser>
{
    protected override IEnumerable<ISyntacticDisableQuantitySumParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticDisableQuantitySumParser>()
    };
}
