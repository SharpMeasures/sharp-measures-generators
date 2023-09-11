namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class CompilationStore
{
    private static Compilation EmptyCompilation { get; set; } = CreateEmptyCompilation();

    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion.CSharp11);
    private static CSharpCompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

    public static Compilation GetCompilation(params string[] sources) => GetCompilation(sources as IEnumerable<string>);
    public static Compilation GetCompilation(IEnumerable<string> sources)
    {
        var syntaxTrees = sources.Select((source) => CSharpSyntaxTree.ParseText(source, ParseOptions));

        return EmptyCompilation.AddSyntaxTrees(syntaxTrees);
    }

    public static async Task<Compilation> GetCompilation(IEnumerable<string> localSources, IEnumerable<string> foreignSources)
    {
        if (foreignSources.Any() is false)
        {
            return GetCompilation(localSources);
        }

        var (_, project) = WorkspaceStore.Create(localSources, foreignSources);

        var compilation = await project.GetCompilationAsync() ?? throw new InvalidOperationException($"Could not compile the {nameof(Project)} provided by the {nameof(WorkspaceStore)}.");

        return compilation;
    }

    public static async Task<(Compilation, AttributeData, AttributeSyntax)> GetComponents(string typeName, params string[] sources) => await GetComponents(typeName, sources as IEnumerable<string>);
    public static async Task<(Compilation, AttributeData, AttributeSyntax)> GetComponents(string typeName, IEnumerable<string> sources)
    {
        var compilation = GetCompilation(sources);

        return await GetComponents(typeName, compilation);
    }

    public static async Task<(Compilation, AttributeData, AttributeSyntax)> GetComponents(string typeName, IEnumerable<string> localSources, IEnumerable<string> foreignSources)
    {
        var compilation = await GetCompilation(localSources, foreignSources);

        return await GetComponents(typeName, compilation);
    }

    private static async Task<(Compilation, AttributeData, AttributeSyntax)> GetComponents(string typeName, Compilation compilation)
    {
        var type = compilation.GetTypeByMetadataName(typeName)!;

        var attributeData = type.GetAttributes()[0];

        var syntax = (AttributeSyntax)await attributeData.ApplicationSyntaxReference!.GetSyntaxAsync();

        return (compilation, attributeData, syntax);
    }

    private static Compilation CreateEmptyCompilation()
    {
        var references = ReferenceLister.List(Assembly.GetExecutingAssembly());

        return CSharpCompilation.Create("FakeAssembly", references: references, options: CompilationOptions);
    }
}
