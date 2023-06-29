namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.DefaultUnitInstanceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class DefaultUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_Constructor_String { get; } = new(() => CreateExpectedResult_Constructor_String("A"));
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_Constructor_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String("A", "B"));

    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_UnitInstance_Null { get; } = new(() => CreateExpectedResult_UnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_UnitInstance_EmptyString { get; } = new(() => CreateExpectedResult_UnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_UnitInstance_String { get; } = new(() => CreateExpectedResult_UnitInstance("UnitInstance"));

    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_Symbol_Null { get; } = new(() => CreateExpectedResult_Symbol(null));
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_Symbol_EmptyString { get; } = new(() => CreateExpectedResult_Symbol(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticDefaultUnitInstance>>> Lazy_Symbol_String { get; } = new(() => CreateExpectedResult_Symbol("Symbol"));

    public static Task<ITestData<ISyntacticDefaultUnitInstance>> Constructor_String => Lazy_Constructor_String.Value;
    public static Task<ITestData<ISyntacticDefaultUnitInstance>> Constructor_String_String => Lazy_Constructor_String_String.Value;

    public static Task<ITestData<ISyntacticDefaultUnitInstance>> UnitInstance_Null => Lazy_UnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticDefaultUnitInstance>> UnitInstance_EmptyString => Lazy_UnitInstance_EmptyString.Value;
    public static Task<ITestData<ISyntacticDefaultUnitInstance>> UnitInstance_String => Lazy_UnitInstance_String.Value;

    public static Task<ITestData<ISyntacticDefaultUnitInstance>> Symbol_Null => Lazy_Symbol_Null.Value;
    public static Task<ITestData<ISyntacticDefaultUnitInstance>> Symbol_EmptyString => Lazy_Symbol_EmptyString.Value;
    public static Task<ITestData<ISyntacticDefaultUnitInstance>> Symbol_String => Lazy_Symbol_String.Value;

    private static async Task<ITestData<ISyntacticDefaultUnitInstance>> CreateExpectedResult_Constructor_String(string? unitInstance)
    {
        var source = $$"""
            [SharpMeasures.DefaultUnitInstance({{StringRepresentationFactory.Create(unitInstance)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        SyntacticDefaultUnitInstance expectedResult = new(unitInstance, new DefaultUnitInstanceSyntax(attributeNameLocation, attributeLocation, unitInstanceLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticDefaultUnitInstance>> CreateExpectedResult_Constructor_String_String(string? unitInstance, string? symbol)
    {
        var source = $$"""
            [SharpMeasures.DefaultUnitInstance({{StringRepresentationFactory.Create(unitInstance)}}, {{StringRepresentationFactory.Create(symbol)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var symbolLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        DefaultUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, unitInstanceLocation) { Symbol = symbolLocation };

        SyntacticDefaultUnitInstance expectedResult = new(unitInstance, syntax) { Symbol = symbol };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticDefaultUnitInstance>> CreateExpectedResult_UnitInstance(string? unitInstance) => await CreateExpectedResult_Constructor_String(unitInstance);
    private static async Task<ITestData<ISyntacticDefaultUnitInstance>> CreateExpectedResult_Symbol(string? symbol) => await CreateExpectedResult_Constructor_String_String("A", symbol);

    private sealed class SyntacticDefaultUnitInstance : ISyntacticDefaultUnitInstance
    {
        public string? UnitInstance { get; }
        public string? Symbol { get; init; }

        public IDefaultUnitInstanceSyntax Syntax { get; }

        public SyntacticDefaultUnitInstance(string? unitInstance, IDefaultUnitInstanceSyntax syntax)
        {
            UnitInstance = unitInstance;

            Syntax = syntax;
        }
    }

    private sealed class DefaultUnitInstanceSyntax : AAttributeSyntax, IDefaultUnitInstanceSyntax
    {
        public Location UnitInstance { get; }
        public Location Symbol { get; init; } = Location.None;

        public DefaultUnitInstanceSyntax(Location attributeName, Location attribute, Location unitInstance) : base(attributeName, attribute)
        {
            UnitInstance = unitInstance;
        }
    }
}
