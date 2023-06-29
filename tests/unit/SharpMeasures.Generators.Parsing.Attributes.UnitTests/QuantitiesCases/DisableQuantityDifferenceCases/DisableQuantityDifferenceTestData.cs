namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DisableQuantityDifferenceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class DisableQuantityDifferenceTestData
{
    private static Lazy<Task<ITestData<ISyntacticDisableQuantityDifference>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);

    public static Task<ITestData<ISyntacticDisableQuantityDifference>> Constructor_Empty => Lazy_Constructor_Empty.Value;

    private static async Task<ITestData<ISyntacticDisableQuantityDifference>> CreateExpectedResult_Constructor_Empty()
    {
        var source = """
            [SharpMeasures.DisableQuantityDifference]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        SyntacticDisableQuantityDifference expectedResult = new(new DisableQuantityDifferenceSyntax(attributeNameLocation, attributeLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticDisableQuantityDifference : ISyntacticDisableQuantityDifference
    {
        public IDisableQuantityDifferenceSyntax Syntax { get; }

        public SyntacticDisableQuantityDifference(IDisableQuantityDifferenceSyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class DisableQuantityDifferenceSyntax : AAttributeSyntax, IDisableQuantityDifferenceSyntax
    {
        public DisableQuantityDifferenceSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
