namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class ScalarQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticScalarQuantity>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    private static Lazy<Task<ITestData<ISyntacticScalarQuantity>>> Lazy_Biased_True { get; } = new(() => CreateExpectedResult_Biased(true));
    private static Lazy<Task<ITestData<ISyntacticScalarQuantity>>> Lazy_Biased_False { get; } = new(() => CreateExpectedResult_Biased(false));

    public static Task<ITestData<ISyntacticScalarQuantity>> Constructor_Type => Lazy_Constructor_Type.Value;

    public static Task<ITestData<ISyntacticScalarQuantity>> Biased_True => Lazy_Biased_True.Value;
    public static Task<ITestData<ISyntacticScalarQuantity>> Biased_False => Lazy_Biased_False.Value;

    private static async Task<ITestData<ISyntacticScalarQuantity>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", unitSymbol);

        static ITypeSymbol unitSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticScalarQuantity>> CreateExpectedResult_Constructor_Type(string unit, Func<Compilation, ITypeSymbol> unitSymbol)
    {
        var source = $$"""
            [SharpMeasures.ScalarQuantity<{{unit}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticScalarQuantity expectedResult = new(unitSymbol(compilation), new ScalarQuantitySyntax(attributeNameLocation, attributeLocation, unitLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScalarQuantity>> CreateExpectedResult_Biased(bool biased)
    {
        var source = $$"""
            [SharpMeasures.ScalarQuantity<int>(Biased = {{StringRepresentationFactory.Create(biased)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var biasedLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        ScalarQuantitySyntax syntax = new(attributeNameLocation, attributeLocation, unitLocation) { Biased = biasedLocation };

        SyntacticScalarQuantity expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { Biased = biased };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticScalarQuantity : ISyntacticScalarQuantity
    {
        public ITypeSymbol Unit { get; }
        public bool? Biased { get; init; }

        public IScalarQuantitySyntax Syntax { get; }

        public SyntacticScalarQuantity(ITypeSymbol unit, IScalarQuantitySyntax syntax)
        {
            Unit = unit;

            Syntax = syntax;
        }
    }

    private sealed class ScalarQuantitySyntax : AAttributeSyntax, IScalarQuantitySyntax
    {
        public Location Unit { get; }
        public Location Biased { get; init; } = Location.None;

        public ScalarQuantitySyntax(Location attributeName, Location attribute, Location unit) : base(attributeName, attribute)
        {
            Unit = unit;
        }
    }
}
