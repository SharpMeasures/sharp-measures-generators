namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.ScalarConstantCases;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class ScalarConstantTestData
{
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_Constructor_String_String_Double { get; } = new(() => CreateExpectedResult_Constructor_String_String_Double("A", "B", 3.14));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_Constructor_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String("A", "B", "C"));

    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_UnitInstance_Null { get; } = new(() => CreateExpectedResult_UnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_UnitInstance_Empty { get; } = new(() => CreateExpectedResult_UnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_UnitInstance_String { get; } = new(() => CreateExpectedResult_UnitInstance("A"));

    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_DoubleValue { get; } = new(() => CreateExpectedResult_DoubleValue(3.14));

    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_StringValue_Null { get; } = new(() => CreateExpectedResult_StringValue(null));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_StringValue_Empty { get; } = new(() => CreateExpectedResult_StringValue(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScalarConstant>>> Lazy_StringValue_String { get; } = new(() => CreateExpectedResult_StringValue("A"));

    public static Task<ITestData<ISyntacticScalarConstant>> Constructor_String_String_Double => Lazy_Constructor_String_String_Double.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> Constructor_String_String_String => Lazy_Constructor_String_String_String.Value;

    public static Task<ITestData<ISyntacticScalarConstant>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticScalarConstant>> UnitInstance_Null => Lazy_UnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> UnitInstance_Empty => Lazy_UnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> UnitInstance_String => Lazy_UnitInstance_String.Value;

    public static Task<ITestData<ISyntacticScalarConstant>> DoubleValue => Lazy_DoubleValue.Value;

    public static Task<ITestData<ISyntacticScalarConstant>> StringValue_Null => Lazy_StringValue_Null.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> StringValue_Empty => Lazy_StringValue_Empty.Value;
    public static Task<ITestData<ISyntacticScalarConstant>> StringValue_String => Lazy_StringValue_String.Value;

    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_Constructor_String_String_Double(string? name, string? unitInstance, double value)
    {
        var source = $$"""
            [SharpMeasures.ScalarConstant({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(unitInstance)}}, {{value}}))]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var valueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        ScalarConstantSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstanceLocation, valueLocation);

        SyntacticScalarConstant expectedResult = new(name, unitInstance, value, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_Constructor_String_String_String(string? name, string? unitInstance, string? value)
    {
        var source = $$"""
            [SharpMeasures.ScalarConstant({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(unitInstance)}}, {{StringRepresentationFactory.Create(value)}}))]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var valueLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        ScalarConstantSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstanceLocation, valueLocation);

        SyntacticScalarConstant expectedResult = new(name, unitInstance, value, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String_String(name, "A", "B");
    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_UnitInstance(string? unitInstance) => await CreateExpectedResult_Constructor_String_String_String("A", unitInstance, "B");

    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_DoubleValue(double value) => await CreateExpectedResult_Constructor_String_String_Double("A", "B", value);
    private static async Task<ITestData<ISyntacticScalarConstant>> CreateExpectedResult_StringValue(string? value) => await CreateExpectedResult_Constructor_String_String_String("A", "B", value);

    private sealed class SyntacticScalarConstant : ISyntacticScalarConstant
    {
        public string? Name { get; }
        public string? UnitInstance { get; }
        public OneOf<double, string?> Value { get; }

        public IScalarConstantSyntax Syntax { get; }

        public SyntacticScalarConstant(string? name, string? unitInstance, OneOf<double, string?> value, IScalarConstantSyntax syntax)
        {
            Name = name;
            UnitInstance = unitInstance;
            Value = value;

            Syntax = syntax;
        }
    }

    private sealed class ScalarConstantSyntax : AAttributeSyntax, IScalarConstantSyntax
    {
        public Location Name { get; }
        public Location UnitInstance { get; }
        public Location Value { get; }

        public ScalarConstantSyntax(Location attributeName, Location attribute, Location name, Location unitInstance, Location value) : base(attributeName, attribute)
        {
            Name = name;
            UnitInstance = unitInstance;
            Value = value;
        }
    }
}
