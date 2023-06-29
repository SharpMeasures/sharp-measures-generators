namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DefaultUnitInstanceCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticDefaultUnitInstanceParser>
{
    protected override IEnumerable<ISemanticDefaultUnitInstanceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticDefaultUnitInstanceParser>()
    };
}
