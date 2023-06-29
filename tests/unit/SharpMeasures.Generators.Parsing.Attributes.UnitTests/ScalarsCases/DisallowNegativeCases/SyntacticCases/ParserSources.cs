﻿namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.DisallowNegativeCases.SyntacticCases;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.TestUtility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed class ParserSources : ATestDataset<ISyntacticDisallowNegativeParser>
{
    protected override IEnumerable<ISyntacticDisallowNegativeParser> GetSamples() => new[]
    {
        DependencyInjection.GetRequiredService<ISyntacticDisallowNegativeParser>()
    };
}