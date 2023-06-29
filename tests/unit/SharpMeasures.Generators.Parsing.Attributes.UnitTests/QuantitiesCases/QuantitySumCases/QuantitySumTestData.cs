namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantitySumCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class QuantitySumTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantitySum>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticQuantitySum>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticQuantitySum>> CreateExpectedResult_Constructor_Type_Populated() => await CreateExpectedResult_Constructor_Type("int", static (compilation) => compilation.GetSpecialType(SpecialType.System_Int32));
    private static async Task<ITestData<ISyntacticQuantitySum>> CreateExpectedResult_Constructor_Type(string sum, Func<Compilation, ITypeSymbol> sumSymbol)
    {
        var source = $$"""
            [SharpMeasures.QuantitySum<{{sum}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var sumLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticQuantitySum expectedResult = new(sumSymbol(compilation), new QuantitySumSyntax(attributeNameLocation, attributeLocation, sumLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticQuantitySum : ISyntacticQuantitySum
    {
        public ITypeSymbol Sum { get; }

        public IQuantitySumSyntax Syntax { get; }

        public SyntacticQuantitySum(ITypeSymbol sum, IQuantitySumSyntax syntax)
        {
            Sum = sum;

            Syntax = syntax;
        }
    }

    private sealed class QuantitySumSyntax : AAttributeSyntax, IQuantitySumSyntax
    {
        public Location Sum { get; }

        public QuantitySumSyntax(Location attributeName, Location attribute, Location sum) : base(attributeName, attribute)
        {
            Sum = sum;
        }
    }
}
