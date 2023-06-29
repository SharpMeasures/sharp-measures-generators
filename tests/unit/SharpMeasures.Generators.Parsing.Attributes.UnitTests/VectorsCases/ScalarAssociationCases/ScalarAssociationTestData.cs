namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.ScalarAssociationCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Threading.Tasks;

internal static class ScalarAssociationTestData
{
    private static Lazy<Task<ITestData<ISyntacticScalarAssociation>>> Lazy_Constructor_Type { get; } = new(CreateExpectedResult_Constructor_Type_Populated);

    private static Lazy<Task<ITestData<ISyntacticScalarAssociation>>> Lazy_AsComponents_True { get; } = new(() => CreateExpectedResult_AsComponents(true));
    private static Lazy<Task<ITestData<ISyntacticScalarAssociation>>> Lazy_AsComponents_False { get; } = new(() => CreateExpectedResult_AsComponents(false));

    private static Lazy<Task<ITestData<ISyntacticScalarAssociation>>> Lazy_AsMagnitude_True { get; } = new(() => CreateExpectedResult_AsMagnitude(true));
    private static Lazy<Task<ITestData<ISyntacticScalarAssociation>>> Lazy_AsMagnitude_False { get; } = new(() => CreateExpectedResult_AsMagnitude(false));

    public static Task<ITestData<ISyntacticScalarAssociation>> Constructor_Type => Lazy_Constructor_Type.Value;

    public static Task<ITestData<ISyntacticScalarAssociation>> AsComponents_True => Lazy_AsComponents_True.Value;
    public static Task<ITestData<ISyntacticScalarAssociation>> AsComponents_False => Lazy_AsComponents_False.Value;

    public static Task<ITestData<ISyntacticScalarAssociation>> AsMagnitude_True => Lazy_AsMagnitude_True.Value;
    public static Task<ITestData<ISyntacticScalarAssociation>> AsMagnitude_False => Lazy_AsMagnitude_False.Value;

    private static async Task<ITestData<ISyntacticScalarAssociation>> CreateExpectedResult_Constructor_Type_Populated()
    {
        return await CreateExpectedResult_Constructor_Type("int", scalarQuantitySymbol);

        static ITypeSymbol scalarQuantitySymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticScalarAssociation>> CreateExpectedResult_Constructor_Type(string scalarQuantity, Func<Compilation, ITypeSymbol> scalarQuantitySymbol)
    {
        var source = $$"""
            [SharpMeasures.ScalarAssociation<{{scalarQuantity}}>]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var scalarQuantityLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);

        SyntacticScalarAssociation expectedResult = new(scalarQuantitySymbol(compilation), new ScalarAssociationSyntax(attributeNameLocation, attributeLocation, scalarQuantityLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScalarAssociation>> CreateExpectedResult_AsComponents(bool asComponents)
    {
        var source = $$"""
            [SharpMeasures.ScalarAssociation<int>(AsComponents = {{StringRepresentationFactory.Create(asComponents)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var scalarQuantityLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var asComponentsLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        ScalarAssociationSyntax syntax = new(attributeNameLocation, attributeLocation, scalarQuantityLocation) { AsComponents = asComponentsLocation };

        SyntacticScalarAssociation expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { AsComponents = asComponents };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScalarAssociation>> CreateExpectedResult_AsMagnitude(bool asMagnitude)
    {
        var source = $$"""
            [SharpMeasures.ScalarAssociation<int>(AsMagnitude = {{StringRepresentationFactory.Create(asMagnitude)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var scalarQuantityLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var asMagnitudeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        ScalarAssociationSyntax syntax = new(attributeNameLocation, attributeLocation, scalarQuantityLocation) { AsMagnitude = asMagnitudeLocation };

        SyntacticScalarAssociation expectedResult = new(compilation.GetSpecialType(SpecialType.System_Int32), syntax) { AsMagnitude = asMagnitude };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticScalarAssociation : ISyntacticScalarAssociation
    {
        public ITypeSymbol ScalarQuantity { get; }

        public IScalarAssociationSyntax Syntax { get; }

        public bool? AsComponents { get; init; }
        public bool? AsMagnitude { get; init; }

        public SyntacticScalarAssociation(ITypeSymbol scalarQuantity, IScalarAssociationSyntax syntax)
        {
            ScalarQuantity = scalarQuantity;

            Syntax = syntax;
        }
    }

    private sealed class ScalarAssociationSyntax : AAttributeSyntax, IScalarAssociationSyntax
    {
        public Location ScalarQuantity { get; }

        public Location AsComponents { get; init; } = Location.None;
        public Location AsMagnitude { get; init; } = Location.None;

        public ScalarAssociationSyntax(Location attributeName, Location attribute, Location scalarQuantity) : base(attributeName, attribute)
        {
            ScalarQuantity = scalarQuantity;
        }
    }
}
