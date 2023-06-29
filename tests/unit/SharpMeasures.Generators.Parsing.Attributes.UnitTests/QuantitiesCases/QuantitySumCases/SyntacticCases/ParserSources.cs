namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantitySumCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticQuantitySumParser>
{
    protected override IEnumerable<ISyntacticQuantitySumParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticQuantitySumParser>()
    };
}
