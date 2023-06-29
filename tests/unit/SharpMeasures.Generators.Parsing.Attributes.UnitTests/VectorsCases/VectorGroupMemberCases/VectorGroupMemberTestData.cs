namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorGroupMemberCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

internal static class VectorGroupMemberTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorGroupMember>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    private static Lazy<Task<ITestData<ISyntacticVectorGroupMember>>> Lazy_Dimension { get; } = new(() => CreateExpectedResult_Dimension(3));

    public static Task<ITestData<ISyntacticVectorGroupMember>> Constructor_Type => Lazy_Constructor_Type.Value;

    public static Task<ITestData<ISyntacticVectorGroupMember>> Dimension => Lazy_Dimension.Value;

    private static async Task<ITestData<ISyntacticVectorGroupMember>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", unitSymbol);

        static ITypeSymbol unitSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticVectorGroupMember>> CreateExpectedResult_Constructor_Type(string group, Func<Compilation, ITypeSymbol> groupSymbol)
    {
        var source = $$"""
            [SharpMeasures.VectorGroupMember<{{group}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var groupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticVectorGroupMember expectedResult = new(groupSymbol(compilation), new VectorGroupSyntax(attributeNameLocation, attributeLocation, groupLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticVectorGroupMember>> CreateExpectedResult_Dimension(int dimension)
    {
        var source = $$"""
            [SharpMeasures.VectorGroupMember<int>(Dimension = {{dimension}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var groupLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var dimensionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        VectorGroupSyntax syntax = new(attributeNameLocation, attributeLocation, groupLocation) { Dimension = dimensionLocation };

        SyntacticVectorGroupMember expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { Dimension = dimension };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticVectorGroupMember : ISyntacticVectorGroupMember
    {
        public ITypeSymbol Group { get; }

        [SuppressMessage("Critical Code Smell", "S3218: Inner class members should not shadow outer class \"static\" or type members", Justification = "Outer member should not be accessed from scope.")]
        public int? Dimension { get; init; }

        public IVectorGroupMemberSyntax Syntax { get; }

        public SyntacticVectorGroupMember(ITypeSymbol group, IVectorGroupMemberSyntax syntax)
        {
            Group = group;

            Syntax = syntax;
        }
    }

    private sealed class VectorGroupSyntax : AAttributeSyntax, IVectorGroupMemberSyntax
    {
        public Location Group { get; }

        [SuppressMessage("Critical Code Smell", "S3218: Inner class members should not shadow outer class \"static\" or type members", Justification = "Outer member should not be accessed from scope.")]
        public Location Dimension { get; init; } = Location.None;

        public VectorGroupSyntax(Location attributeName, Location attribute, Location group) : base(attributeName, attribute)
        {
            Group = group;
        }
    }
}
