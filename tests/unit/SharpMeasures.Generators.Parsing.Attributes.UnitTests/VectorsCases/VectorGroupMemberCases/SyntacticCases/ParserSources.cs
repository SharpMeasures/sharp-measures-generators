namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorGroupMemberCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticVectorGroupMemberParser>
{
    protected override IEnumerable<ISyntacticVectorGroupMemberParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticVectorGroupMemberParser>()
    };
}
