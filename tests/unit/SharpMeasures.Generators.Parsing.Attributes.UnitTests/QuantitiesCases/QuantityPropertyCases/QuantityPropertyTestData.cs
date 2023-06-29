namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityPropertyCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class QuantityPropertyTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Constructor_Type_String_String { get; } = new(CreateExpectedResult_Constructor_Type_String_String_Populated);

    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Expression_Null { get; } = new(() => CreateExpectedResult_Expression(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Expression_Empty { get; } = new(() => CreateExpectedResult_Expression(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityProperty>>> Lazy_Expression_String { get; } = new(() => CreateExpectedResult_Expression("A"));

    public static Task<ITestData<ISyntacticQuantityProperty>> Constructor_Type_String_String => Lazy_Constructor_Type_String_String.Value;

    public static Task<ITestData<ISyntacticQuantityProperty>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProperty>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProperty>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticQuantityProperty>> Expression_Null => Lazy_Expression_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProperty>> Expression_Empty => Lazy_Expression_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProperty>> Expression_String => Lazy_Expression_String.Value;

    private static async Task<ITestData<ISyntacticQuantityProperty>> CreateExpectedResult_Constructor_Type_String_String_Populated()
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProperty>> CreateExpectedResult_Constructor(string result, string? name, string? expression, Func<Compilation, ITypeSymbol> resultSymbol)
    {
        var source = $$"""
            [SharpMeasures.QuantityProperty<{{result}}>({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(expression)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        QuantityPropertySyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, nameLocation, expressionLocation);

        SyntacticQuantityProperty expectedResult = new(resultSymbol(compilation), name, expression, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticQuantityProperty>> CreateExpectedResult_Name(string? name)
    {
        return await CreateExpectedResult_Constructor("int", name, "B", resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProperty>> CreateExpectedResult_Expression(string? expression)
    {
        return await CreateExpectedResult_Constructor("int", "A", expression, resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private sealed class SyntacticQuantityProperty : ISyntacticQuantityProperty
    {
        public ITypeSymbol Result { get; }

        public string? Name { get; }
        public string? Expression { get; }

        public IQuantityPropertySyntax Syntax { get; init; }

        public SyntacticQuantityProperty(ITypeSymbol result, string? name, string? expression, IQuantityPropertySyntax syntax)
        {
            Result = result;

            Name = name;
            Expression = expression;

            Syntax = syntax;
        }
    }

    private sealed class QuantityPropertySyntax : AAttributeSyntax, IQuantityPropertySyntax
    {
        public Location Result { get; }

        public Location Name { get; }
        public Location Expression { get; }

        public QuantityPropertySyntax(Location attributeName, Location attribute, Location result, Location name, Location expression) : base(attributeName, attribute)
        {
            Result = result;

            Name = name;
            Expression = expression;
        }
    }
}
