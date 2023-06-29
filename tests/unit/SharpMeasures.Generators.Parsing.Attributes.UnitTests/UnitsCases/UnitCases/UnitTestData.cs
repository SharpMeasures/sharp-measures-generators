namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class UnitTestData
{
    private static Lazy<Task<ITestData<ISyntacticUnit>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    private static Lazy<Task<ITestData<ISyntacticUnit>>> Lazy_BiasTerm_True { get; } = new(() => CreateExpectedResult_BiasTerm(true));
    private static Lazy<Task<ITestData<ISyntacticUnit>>> Lazy_BiasTerm_False { get; } = new(() => CreateExpectedResult_BiasTerm(false));

    public static Task<ITestData<ISyntacticUnit>> Constructor_Type => Lazy_Constructor_Type.Value;

    public static Task<ITestData<ISyntacticUnit>> BiasTerm_True => Lazy_BiasTerm_True.Value;
    public static Task<ITestData<ISyntacticUnit>> BiasTerm_False => Lazy_BiasTerm_False.Value;

    private static async Task<ITestData<ISyntacticUnit>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", scalarQuantitySymbol);

        static ITypeSymbol scalarQuantitySymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticUnit>> CreateExpectedResult_Constructor_Type(string scalarQuantity, Func<Compilation, ITypeSymbol> scalarQuantitySymbol)
    {
        var source = $$"""
            [SharpMeasures.Unit<{{scalarQuantity}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var scalarQuantityLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        SyntacticUnit expectedResult = new(scalarQuantitySymbol(compilation), new UnitSyntax(attributeNameLocation, attributeLocation, scalarQuantityLocation));
        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticUnit>> CreateExpectedResult_BiasTerm(bool biasTerm)
    {
        var source = $$"""
            [SharpMeasures.Unit<int>(BiasTerm = {{StringRepresentationFactory.Create(biasTerm)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var scalarQuantity = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var biasedLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        UnitSyntax syntax = new(attributeNameLocation, attributeLocation, scalarQuantity) { BiasTerm = biasedLocation };

        SyntacticUnit expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { BiasTerm = biasTerm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticUnit : ISyntacticUnit
    {
        public ITypeSymbol ScalarQuantity { get; }
        public bool? BiasTerm { get; init; }

        public IUnitSyntax Syntax { get; }

        public SyntacticUnit(ITypeSymbol scalarQuantity, IUnitSyntax syntax)
        {
            ScalarQuantity = scalarQuantity;

            Syntax = syntax;
        }
    }

    private sealed class UnitSyntax : AAttributeSyntax, IUnitSyntax
    {
        public Location ScalarQuantity { get; }
        public Location BiasTerm { get; init; } = Location.None;

        public UnitSyntax(Location attributeName, Location attribute, Location scalarQuantity) : base(attributeName, attribute)
        {
            ScalarQuantity = scalarQuantity;
        }
    }
}
