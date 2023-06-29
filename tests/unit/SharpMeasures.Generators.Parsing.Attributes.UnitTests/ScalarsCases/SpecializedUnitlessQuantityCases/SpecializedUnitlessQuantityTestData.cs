namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.SpecializedUnitlessQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class SpecializedUnitlessQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticSpecializedUnitlessQuantity>>> Lazy_Constructor_Type { get; } = new(CreateExpectedType_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticSpecializedUnitlessQuantity>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticSpecializedUnitlessQuantity>> CreateExpectedType_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", originalSymbol);

        static ITypeSymbol originalSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticSpecializedUnitlessQuantity>> CreateExpectedResult_Constructor_Type(string original, Func<Compilation, ITypeSymbol> originalSymbol)
    {
        var source = $$"""
            [SharpMeasures.SpecializedUnitlessQuantity<{{original}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var originalLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticSpecializedUnitlessQuantity expectedResult = new(originalSymbol(compilation), new SpecializedUnitlessQuantitySyntax(attributeNameLocation, attributeLocation, originalLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticSpecializedUnitlessQuantity : ISyntacticSpecializedUnitlessQuantity
    {
        public ITypeSymbol Original { get; }

        public ISpecializedUnitlessQuantitySyntax Syntax { get; }

        public SyntacticSpecializedUnitlessQuantity(ITypeSymbol original, ISpecializedUnitlessQuantitySyntax syntax)
        {
            Original = original;

            Syntax = syntax;
        }
    }

    private sealed class SpecializedUnitlessQuantitySyntax : AAttributeSyntax, ISpecializedUnitlessQuantitySyntax
    {
        public Location Original { get; }

        public SpecializedUnitlessQuantitySyntax(Location attributeName, Location attribute, Location original) : base(attributeName, attribute)
        {
            Original = original;
        }
    }
}
