namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.TestUtility;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

internal static class CompilationStore
{
    private static IDictionary<string, Compilation> Compilations { get; } = new ConcurrentDictionary<string, Compilation>();

    public static async Task<Compilation> GetCompilation(string source)
    {
        if (Compilations.TryGetValue(source, out var cachedCompilation))
        {
            return cachedCompilation;
        }

        var compilation = await StringCompilationFactory.Create(source);

        Compilations[source] = compilation;

        return compilation;
    }

    public static async Task<(Compilation, AttributeData, AttributeSyntax)> GetComponents(string localSource, string typeName)
    {
        var compilation = await GetCompilation(localSource);

        var type = compilation.GetTypeByMetadataName(typeName)!;

        var attributeData = type.GetAttributes()[0];

        var syntax = (AttributeSyntax)await attributeData.ApplicationSyntaxReference!.GetSyntaxAsync();

        return (compilation, attributeData, syntax);
    }
}
