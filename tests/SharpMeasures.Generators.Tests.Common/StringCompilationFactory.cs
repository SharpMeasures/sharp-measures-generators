namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class StringCompilationFactory
{
    private static Compilation? EmptyCompilation { get; set; }

    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion.Preview);
    private static CSharpCompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

    public static Task<Compilation> Create(IEnumerable<string> sources)
    {
        var emptyCompilation = EmptyCompilation ??= CreateEmptyCompilation();

        var syntaxTrees = sources.Select((source) => CSharpSyntaxTree.ParseText(source, ParseOptions));

        return Task.FromResult(emptyCompilation.AddSyntaxTrees(syntaxTrees));
    }

    public static async Task<Compilation> Create(params string[] sources) => await Create(sources as IEnumerable<string>).ConfigureAwait(false);

    public static async Task<Compilation> Create(IEnumerable<string> localSources, IEnumerable<string> foreignSources)
    {
        if (foreignSources.Any() is false)
        {
            return await Create(localSources).ConfigureAwait(false);
        }

        var (_, project) = StringWorkspaceFactory.Create(localSources, foreignSources);

        var compilation = await project.GetCompilationAsync().ConfigureAwait(false) ?? throw new InvalidOperationException($"Could not compile the {nameof(Project)} provided by the {nameof(StringWorkspaceFactory)}.");

        return compilation;
    }

    private static Compilation CreateEmptyCompilation() => CSharpCompilation.Create("FakeAssembly", references: GetMetadataReferences(), options: CompilationOptions);

    private static IEnumerable<MetadataReference> GetMetadataReferences()
    {
        var assemblyReferences = ReferenceLister.List(Assembly.GetEntryAssembly()!);

        return assemblyReferences
            .Where(static (assembly) => assembly.IsDynamic is false)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();
    }
}
