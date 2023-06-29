namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityProcessCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
public sealed class ParserSources : ATestDataset<ISemanticQuantityProcessParser>
{
    protected override IEnumerable<ISemanticQuantityProcessParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticQuantityProcessParser>()
    };
}
