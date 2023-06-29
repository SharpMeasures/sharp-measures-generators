namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantitySumCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class DisableQuantitySumTestData
{
    private static Lazy<Task<ITestData<ISyntacticDisableQuantitySum>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);

    public static Task<ITestData<ISyntacticDisableQuantitySum>> Constructor_Empty => Lazy_Constructor_Empty.Value;

    private static async Task<ITestData<ISyntacticDisableQuantitySum>> CreateExpectedResult_Constructor_Empty()
    {
        var source = """
            [SharpMeasures.DisableQuantitySum]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        SyntacticDisableQuantitySum expectedResult = new(new DisableQuantitySumSyntax(attributeNameLocation, attributeLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticDisableQuantitySum : ISyntacticDisableQuantitySum
    {
        public IDisableQuantitySumSyntax Syntax { get; }

        public SyntacticDisableQuantitySum(IDisableQuantitySumSyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class DisableQuantitySumSyntax : AAttributeSyntax, IDisableQuantitySumSyntax
    {
        public DisableQuantitySumSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
