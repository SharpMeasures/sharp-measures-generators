namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.PrefixedUnitInstanceCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticPrefixedUnitInstanceParser>
{
    protected override IEnumerable<ISyntacticPrefixedUnitInstanceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticPrefixedUnitInstanceParser>()
    };
}
