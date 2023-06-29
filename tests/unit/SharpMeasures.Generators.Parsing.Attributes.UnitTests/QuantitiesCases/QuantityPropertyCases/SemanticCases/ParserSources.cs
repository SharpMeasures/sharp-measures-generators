namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityPropertyCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
public sealed class ParserSources : ATestDataset<ISemanticQuantityPropertyParser>
{
    protected override IEnumerable<ISemanticQuantityPropertyParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticQuantityPropertyParser>()
    };
}
