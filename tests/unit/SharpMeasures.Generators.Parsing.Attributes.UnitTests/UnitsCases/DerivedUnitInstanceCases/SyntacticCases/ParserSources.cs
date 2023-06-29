namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.DerivedUnitInstanceCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticDerivedUnitInstanceParser>
{
    protected override IEnumerable<ISyntacticDerivedUnitInstanceParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticDerivedUnitInstanceParser>()
    };
}
