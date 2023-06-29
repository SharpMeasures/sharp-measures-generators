namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.SpecializedVectorGroupCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Threading.Tasks;

internal static class SpecializedVectorGroupTestData
{
    private static Lazy<Task<ITestData<ISyntacticSpecializedVectorGroup>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticSpecializedVectorGroup>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticSpecializedVectorGroup>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", originalSymbol);

        static ITypeSymbol originalSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticSpecializedVectorGroup>> CreateExpectedResult_Constructor_Type(string original, Func<Compilation, ITypeSymbol> originalSymbol)
    {
        var source = $$"""
            [SharpMeasures.SpecializedVectorGroup<{{original}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var originalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticSpecializedVectorGroup expectedResult = new(originalSymbol(compilation), new SpecializedVectorGroupSyntax(attributeNameLocation, attributeLocation, originalLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticSpecializedVectorGroup : ISyntacticSpecializedVectorGroup
    {
        public ITypeSymbol Original { get; }

        public ISpecializedVectorGroupSyntax Syntax { get; }

        public SyntacticSpecializedVectorGroup(ITypeSymbol original, ISpecializedVectorGroupSyntax syntax)
        {
            Original = original;

            Syntax = syntax;
        }
    }

    private sealed class SpecializedVectorGroupSyntax : AAttributeSyntax, ISpecializedVectorGroupSyntax
    {
        public Location Original { get; }

        public SpecializedVectorGroupSyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }
    }
}
