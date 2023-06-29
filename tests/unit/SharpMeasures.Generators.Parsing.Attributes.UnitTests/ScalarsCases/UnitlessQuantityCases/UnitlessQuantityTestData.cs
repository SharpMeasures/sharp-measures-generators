namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.UnitlessQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class UnitlessQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticUnitlessQuantity>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);

    public static Task<ITestData<ISyntacticUnitlessQuantity>> Constructor_Empty => Lazy_Constructor_Empty.Value;

    private static async Task<ITestData<ISyntacticUnitlessQuantity>> CreateExpectedResult_Constructor_Empty()
    {
        var source = """
            [SharpMeasures.UnitlessQuantity]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        SyntacticUnitlessQuantity expectedResult = new(new UnitlessQuantitySyntax(attributeNameLocation, attributeLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticUnitlessQuantity : ISyntacticUnitlessQuantity
    {
        public IUnitlessQuantitySyntax Syntax { get; }

        public SyntacticUnitlessQuantity(IUnitlessQuantitySyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class UnitlessQuantitySyntax : AAttributeSyntax, IUnitlessQuantitySyntax
    {
        public UnitlessQuantitySyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
