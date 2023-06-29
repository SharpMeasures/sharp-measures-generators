namespace SharpMeasures.Generators.Parsing.Attributes.DocumentationCases.GenerateDocumentationCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Documentation;

using System;
using System.Threading.Tasks;

internal static class GenerateDocumentationTestData
{
    private static Lazy<Task<ITestData<ISyntacticGenerateDocumentation>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);
    private static Lazy<Task<ITestData<ISyntacticGenerateDocumentation>>> Lazy_Constructor_Bool { get; } = new(() => CreateExpectedResult_Constructor_Bool(true));

    private static Lazy<Task<ITestData<ISyntacticGenerateDocumentation>>> Lazy_Generate_True { get; } = new(() => CreateExpectedResult_Generate(true));
    private static Lazy<Task<ITestData<ISyntacticGenerateDocumentation>>> Lazy_Generate_False { get; } = new(() => CreateExpectedResult_Generate(false));

    public static Task<ITestData<ISyntacticGenerateDocumentation>> Constructor_Empty => Lazy_Constructor_Empty.Value;
    public static Task<ITestData<ISyntacticGenerateDocumentation>> Constructor_Bool => Lazy_Constructor_Bool.Value;

    public static Task<ITestData<ISyntacticGenerateDocumentation>> Generate_True => Lazy_Generate_True.Value;
    public static Task<ITestData<ISyntacticGenerateDocumentation>> Generate_False => Lazy_Generate_False.Value;

    private static async Task<ITestData<ISyntacticGenerateDocumentation>> CreateExpectedResult_Constructor_Empty()
    {
        var source = """
            [SharpMeasures.GenerateDocumentation]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        GenerateDocumentationSyntax syntax = new(attributeNameLocation, attributeLocation);

        SyntacticGenerateDocumentation expectedResult = new(syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticGenerateDocumentation>> CreateExpectedResult_Constructor_Bool(bool generate)
    {
        var source = $$"""
            [SharpMeasures.GenerateDocumentation({{StringRepresentationFactory.Create(generate)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var generateLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        GenerateDocumentationSyntax syntax = new(attributeNameLocation, attributeLocation) { Generate = generateLocation };

        SyntacticGenerateDocumentation expectedResult = new(syntax) { Generate = generate };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticGenerateDocumentation>> CreateExpectedResult_Generate(bool generate) => await CreateExpectedResult_Constructor_Bool(generate);

    private sealed class SyntacticGenerateDocumentation : ISyntacticGenerateDocumentation
    {
        public bool? Generate { get; init; }
        public IGenerateDocumentationSyntax Syntax { get; }

        public SyntacticGenerateDocumentation(IGenerateDocumentationSyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class GenerateDocumentationSyntax : AAttributeSyntax, IGenerateDocumentationSyntax
    {
        public Location Generate { get; init; } = Location.None;

        public GenerateDocumentationSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
