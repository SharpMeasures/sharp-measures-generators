namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityDifferenceCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticQuantityDifferenceParser>
{
    protected override IEnumerable<ISemanticQuantityDifferenceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticQuantityDifferenceParser>()
    };
}
