namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.BiasedUnitInstanceCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticBiasedUnitInstanceParser>
{
    protected override IEnumerable<ISyntacticBiasedUnitInstanceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticBiasedUnitInstanceParser>()
    };
}
