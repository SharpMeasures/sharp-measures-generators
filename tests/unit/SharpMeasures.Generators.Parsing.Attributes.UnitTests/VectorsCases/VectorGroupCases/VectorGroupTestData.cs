namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorGroupCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Threading.Tasks;

internal static class VectorGroupTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorGroup>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticVectorGroup>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticVectorGroup>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", unitSymbol);

        static ITypeSymbol unitSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticVectorGroup>> CreateExpectedResult_Constructor_Type(string unit, Func<Compilation, ITypeSymbol> unitSymbol)
    {
        var source = $$"""
            [SharpMeasures.VectorGroup<{{unit}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticVectorGroup expectedResult = new(unitSymbol(compilation), new VectorGroupSyntax(attributeNameLocation, attributeLocation, unitLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticVectorGroup : ISyntacticVectorGroup
    {
        public ITypeSymbol Unit { get; }

        public IVectorGroupSyntax Syntax { get; }

        public SyntacticVectorGroup(ITypeSymbol unit, IVectorGroupSyntax syntax)
        {
            Unit = unit;

            Syntax = syntax;
        }
    }

    private sealed class VectorGroupSyntax : AAttributeSyntax, IVectorGroupSyntax
    {
        public Location Unit { get; }

        public VectorGroupSyntax(Location attributeName, Location attribute, Location unit) : base(attributeName, attribute)
        {
            Unit = unit;
        }
    }
}
