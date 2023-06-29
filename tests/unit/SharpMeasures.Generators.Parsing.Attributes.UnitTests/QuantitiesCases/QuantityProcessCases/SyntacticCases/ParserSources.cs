namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityProcessCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
public sealed class ParserSources : ATestDataset<ISyntacticQuantityProcessParser>
{
    protected override IEnumerable<ISyntacticQuantityProcessParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticQuantityProcessParser>()
    };
}
