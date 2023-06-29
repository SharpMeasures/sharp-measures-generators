namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.ScalarAssociationCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticScalarAssociationParser>
{
    protected override IEnumerable<ISyntacticScalarAssociationParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticScalarAssociationParser>()
    };
}
