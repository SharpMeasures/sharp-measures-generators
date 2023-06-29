namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorQuantityCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

internal static class VectorQuantityTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorQuantity>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    private static Lazy<Task<ITestData<ISyntacticVectorQuantity>>> Lazy_Dimension { get; } = new(() => CreateExpectedResult_Dimension(3));

    public static Task<ITestData<ISyntacticVectorQuantity>> Constructor_Type => Lazy_Constructor_Type.Value;

    public static Task<ITestData<ISyntacticVectorQuantity>> Dimension => Lazy_Dimension.Value;

    private static async Task<ITestData<ISyntacticVectorQuantity>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", unitSymbol);

        static ITypeSymbol unitSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticVectorQuantity>> CreateExpectedResult_Constructor_Type(string unit, Func<Compilation, ITypeSymbol> unitSymbol)
    {
        var source = $$"""
            [SharpMeasures.VectorQuantity<{{unit}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticVectorQuantity expectedResult = new(unitSymbol(compilation), new VectorGroupSyntax(attributeNameLocation, attributeLocation, unitLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticVectorQuantity>> CreateExpectedResult_Dimension(int dimension)
    {
        var source = $$"""
            [SharpMeasures.VectorQuantity<int>(Dimension = {{dimension}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var dimensionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        VectorGroupSyntax syntax = new(attributeNameLocation, attributeLocation, unitLocation) { Dimension = dimensionLocation };

        SyntacticVectorQuantity expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { Dimension = dimension };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticVectorQuantity : ISyntacticVectorQuantity
    {
        public ITypeSymbol Unit { get; }

        [SuppressMessage("Critical Code Smell", "S3218: Inner class members should not shadow outer class \"static\" or type members", Justification = "Outer member should not be accessed from scope.")]
        public int? Dimension { get; init; }

        public IVectorQuantitySyntax Syntax { get; }

        public SyntacticVectorQuantity(ITypeSymbol unit, IVectorQuantitySyntax syntax)
        {
            Unit = unit;

            Syntax = syntax;
        }
    }

    private sealed class VectorGroupSyntax : AAttributeSyntax, IVectorQuantitySyntax
    {
        public Location Unit { get; }

        [SuppressMessage("Critical Code Smell", "S3218: Inner class members should not shadow outer class \"static\" or type members", Justification = "Outer member should not be accessed from scope.")]
        public Location Dimension { get; init; } = Location.None;

        public VectorGroupSyntax(Location attributeName, Location attribute, Location unit) : base(attributeName, attribute)
        {
            Unit = unit;
        }
    }
}
