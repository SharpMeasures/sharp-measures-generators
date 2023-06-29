namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.SpecializedVectorGroupCases.SemanticCases;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISemanticSpecializedVectorGroupParser>
{
    protected override IEnumerable<ISemanticSpecializedVectorGroupParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISemanticSpecializedVectorGroupParser>()
    };
}
