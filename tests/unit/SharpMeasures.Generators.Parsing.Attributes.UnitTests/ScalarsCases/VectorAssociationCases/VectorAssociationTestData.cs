namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.VectorAssociationCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class VectorAssociationTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorAssociation>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    public static Task<ITestData<ISyntacticVectorAssociation>> Constructor_Type => Lazy_Constructor_Type.Value;

    private static async Task<ITestData<ISyntacticVectorAssociation>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", vectorQuantitySymbol);

        static ITypeSymbol vectorQuantitySymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticVectorAssociation>> CreateExpectedResult_Constructor_Type(string vectorQuantity, Func<Compilation, ITypeSymbol> vectorQuantitySymbol)
    {
        var source = $$"""
            [SharpMeasures.VectorAssociation<{{vectorQuantity}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var vectorQuantityLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticVectorAssociation expectedResult = new(vectorQuantitySymbol(compilation), new VectorAssociationSyntax(attributeNameLocation, attributeLocation, vectorQuantityLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticVectorAssociation : ISyntacticVectorAssociation
    {
        public ITypeSymbol VectorQuantity { get; }

        public IVectorAssociationSyntax Syntax { get; }

        public SyntacticVectorAssociation(ITypeSymbol vectorQuantity, IVectorAssociationSyntax syntax)
        {
            VectorQuantity = vectorQuantity;

            Syntax = syntax;
        }
    }

    private sealed class VectorAssociationSyntax : AAttributeSyntax, IVectorAssociationSyntax
    {
        public Location VectorQuantity { get; }

        public VectorAssociationSyntax(Location attributeName, Location attribute, Location vectorQuantity) : base(attributeName, attribute)
        {
            VectorQuantity = vectorQuantity;
        }
    }
}
