namespace SharpMeasures.Generators.TestUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class StringWorkspaceFactory
{
    private static CSharpParseOptions ParseOptions { get; } = new(languageVersion: LanguageVersion.Preview);
    private static CSharpCompilationOptions CompilationOptions { get; } = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

    public static (Workspace Workspace, Project Project) Create(IEnumerable<string> sources)
    {
        if (sources is null)
        {
            throw new ArgumentNullException(nameof(sources));
        }

        return Create(ToDictionary(sources));
    }

    public static (Workspace Workspace, Project Project) Create(params string[] sources) => Create(sources as IEnumerable<string>);

    public static (Workspace Workspace, Project Project) Create(IReadOnlyDictionary<string, string> namedSources)
    {
        if (namedSources is null)
        {
            throw new ArgumentNullException(nameof(namedSources));
        }

        AdhocWorkspace workspace = new();

        var references = GetMetadataReferences();

        var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);

        var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "FakeAssembly", "FakeAssembly", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: references
        );

        var solution = workspace.AddSolution(solutionInfo);

        solution = solution.AddProject(projectInfo);

        var index = 0;

        foreach (var (name, content) in namedSources)
        {
            var documentID = DocumentId.CreateNewId(projectInfo.Id);

            var path = name switch
            {
                "" => $"Local{index++}.cs",
                not "" => name
            };

            solution = solution.AddDocument(documentID, path, SourceText.From(content));
        }

        workspace.TryApplyChanges(solution);

        var project = workspace.CurrentSolution.GetProject(projectInfo.Id)!;

        return (workspace, project);
    }

    public static (Workspace Workspace, Project Project) Create(IEnumerable<string> localSources, IEnumerable<string> foreignSources) => Create(ToDictionary(localSources), ToDictionary(foreignSources));

    public static (Workspace Workspace, Project Project) Create(IReadOnlyDictionary<string, string> namedLocalSources, IReadOnlyDictionary<string, string> namedForeignSources)
    {
        if (namedLocalSources is null)
        {
            throw new ArgumentNullException(nameof(namedLocalSources));
        }

        if (namedForeignSources is null)
        {
            throw new ArgumentNullException(nameof(namedForeignSources));
        }

        AdhocWorkspace workspace = new();

        var references = GetMetadataReferences();

        var solutionInfo = SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default);

        var localProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "FakeAssembly.Local", "FakeAssembly.Local", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: references
        );

        var foreignProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, "FakeAssembly.Foreign", "FakeAssembly.Foreign", "C#",
            parseOptions: ParseOptions,
            compilationOptions: CompilationOptions,
            metadataReferences: references
        );

        var solution = workspace.AddSolution(solutionInfo);

        solution = solution.AddProject(localProjectInfo);
        solution = solution.AddProject(foreignProjectInfo);

        solution = solution.AddProjectReference(localProjectInfo.Id, new ProjectReference(foreignProjectInfo.Id));

        var index = 0;

        foreach (var (name, content) in namedLocalSources)
        {
            var documentID = DocumentId.CreateNewId(localProjectInfo.Id);

            var path = name switch
            {
                "" => $"Local{index++}.cs",
                not "" => name
            };

            solution = solution.AddDocument(documentID, path, SourceText.From(content));
        }

        foreach (var (name, content) in namedForeignSources)
        {
            var documentID = DocumentId.CreateNewId(foreignProjectInfo.Id);

            var path = name switch
            {
                "" => $"Foreign{index++}.cs",
                not "" => name
            };

            solution = solution.AddDocument(documentID, path, SourceText.From(content));
        }

        workspace.TryApplyChanges(solution);

        var project = solution.GetProject(localProjectInfo.Id)!;

        return (workspace, project);
    }

    private static IReadOnlyDictionary<string, string> ToDictionary(IEnumerable<string> sources)
    {
        return sources.Select(insertName).ToDictionary((source) => source.Name, (source) => source.Content);

        static (string Name, string Content) insertName(string content, int index) => ($"Source{index}.cs", content);
    }

    private static IEnumerable<MetadataReference> GetMetadataReferences()
    {
        var assemblyReferences = ReferenceLister.List(Assembly.GetEntryAssembly()!);

        return assemblyReferences
            .Where(static (assembly) => assembly.IsDynamic is false)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();
    }
}
