namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.VectorAssociationCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticVectorAssociationParser>
{
    protected override IEnumerable<ISemanticVectorAssociationParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticVectorAssociationParser>()
    };
}
