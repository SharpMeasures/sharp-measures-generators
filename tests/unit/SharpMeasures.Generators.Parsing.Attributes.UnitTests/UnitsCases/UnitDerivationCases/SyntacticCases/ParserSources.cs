namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitDerivationCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticUnitDerivationParser>
{
    protected override IEnumerable<ISyntacticUnitDerivationParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticUnitDerivationParser>()
    };
}
