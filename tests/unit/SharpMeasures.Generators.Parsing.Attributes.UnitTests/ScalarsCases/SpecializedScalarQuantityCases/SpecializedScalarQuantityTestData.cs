namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.SpecializedScalarQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class SpecializedScalarQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticSpecializedScalarQuantity>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticSpecializedScalarQuantity>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticSpecializedScalarQuantity>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", originalSymbol);

        static ITypeSymbol originalSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticSpecializedScalarQuantity>> CreateExpectedResult_Constructor_Type(string original, Func<Compilation, ITypeSymbol> originalSymbol)
    {
        var source = $$"""
            [SharpMeasures.SpecializedScalarQuantity<{{original}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var originalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticSpecializedScalarQuantity expectedResult = new(originalSymbol(compilation), new SpecializedScalarQuantitySyntax(attributeNameLocation, attributeLocation, originalLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticSpecializedScalarQuantity : ISyntacticSpecializedScalarQuantity
    {
        public ITypeSymbol Original { get; }

        public ISpecializedScalarQuantitySyntax Syntax { get; }

        public SyntacticSpecializedScalarQuantity(ITypeSymbol original, ISpecializedScalarQuantitySyntax syntax)
        {
            Original = original;

            Syntax = syntax;
        }
    }

    private sealed class SpecializedScalarQuantitySyntax : AAttributeSyntax, ISpecializedScalarQuantitySyntax
    {
        public Location Original { get; }

        public SpecializedScalarQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }
    }
}
