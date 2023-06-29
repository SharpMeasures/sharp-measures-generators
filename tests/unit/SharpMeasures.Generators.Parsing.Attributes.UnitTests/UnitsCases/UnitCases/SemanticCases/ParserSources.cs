namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticUnitParser>
{
    protected override IEnumerable<ISemanticUnitParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticUnitParser>()
    };
}
