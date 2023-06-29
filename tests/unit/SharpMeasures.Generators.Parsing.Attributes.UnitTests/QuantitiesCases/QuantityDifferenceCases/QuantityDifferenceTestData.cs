namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityDifferenceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class QuantityDifferenceTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantityDifference>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticQuantityDifference>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticQuantityDifference>> CreateExpectedResult_Constructor_Type_Populated() => await CreateExpectedResult_Constructor_Type("int", static (compilation) => compilation.GetSpecialType(SpecialType.System_Int32));
    private static async Task<ITestData<ISyntacticQuantityDifference>> CreateExpectedResult_Constructor_Type(string difference, Func<Compilation, ITypeSymbol> differenceSymbol)
    {
        var source = $$"""
            [SharpMeasures.QuantityDifference<{{difference}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var differenceLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticQuantityDifference expectedResult = new(differenceSymbol(compilation), new QuantityDifferenceSyntax(attributeNameLocation, attributeLocation, differenceLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticQuantityDifference : ISyntacticQuantityDifference
    {
        public ITypeSymbol Difference { get; }

        public IQuantityDifferenceSyntax Syntax { get; }

        public SyntacticQuantityDifference(ITypeSymbol difference, IQuantityDifferenceSyntax syntax)
        {
            Difference = difference;

            Syntax = syntax;
        }
    }

    private sealed class QuantityDifferenceSyntax : AAttributeSyntax, IQuantityDifferenceSyntax
    {
        public Location Difference { get; }

        public QuantityDifferenceSyntax(Location attributeName, Location attribute, Location difference) : base(attributeName, attribute)
        {
            Difference = difference;
        }
    }
}
