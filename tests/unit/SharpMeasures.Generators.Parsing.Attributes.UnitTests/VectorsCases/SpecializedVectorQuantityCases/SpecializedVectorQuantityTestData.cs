namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.SpecializedVectorQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Threading.Tasks;

internal static class SpecializedVectorQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticSpecializedVectorQuantity>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticSpecializedVectorQuantity>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticSpecializedVectorQuantity>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", originalSymbol);

        static ITypeSymbol originalSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticSpecializedVectorQuantity>> CreateExpectedResult_Constructor_Type(string original, Func<Compilation, ITypeSymbol> originalSymbol)
    {
        var source = $$"""
            [SharpMeasures.SpecializedVectorQuantity<{{original}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var originalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticSpecializedVectorQuantity expectedResult = new(originalSymbol(compilation), new SpecializedVectorQuantitySyntax(attributeNameLocation, attributeLocation, originalLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticSpecializedVectorQuantity : ISyntacticSpecializedVectorQuantity
    {
        public ITypeSymbol Original { get; }

        public ISpecializedVectorQuantitySyntax Syntax { get; }

        public SyntacticSpecializedVectorQuantity(ITypeSymbol original, ISpecializedVectorQuantitySyntax syntax)
        {
            Original = original;

            Syntax = syntax;
        }
    }

    private sealed class SpecializedVectorQuantitySyntax : AAttributeSyntax, ISpecializedVectorQuantitySyntax
    {
        public Location Original { get; }

        public SpecializedVectorQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }
    }
}
