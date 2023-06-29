namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.AliasedUnitInstanceCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticAliasedUnitInstanceParser>
{
    protected override IEnumerable<ISyntacticAliasedUnitInstanceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticAliasedUnitInstanceParser>()
    };
}
