namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.ScaledUnitInstanceCases;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class ScaledUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Constructor_String_String_Double { get; } = new(() => CreateExpectedResult_Constructor_String_String_Double("A", "B", 3.14));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Constructor_String_String_String_Double { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_Double("A", "B", "C", 3.14));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Constructor_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String("A", "B", "C"));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Constructor_String_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_String("A", "B", "C", "D"));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_OriginalUnitInstance_Null { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_OriginalUnitInstance_Empty { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_OriginalUnitInstance_String { get; } = new(() => CreateExpectedResult_OriginalUnitInstance("A"));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_DoubleScale { get; } = new(() => CreateExpectedResult_DoubleScale(3.14));

    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_StringScale_Null { get; } = new(() => CreateExpectedResult_StringScale(null));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_StringScale_Empty { get; } = new(() => CreateExpectedResult_StringScale(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticScaledUnitInstance>>> Lazy_StringScale_String { get; } = new(() => CreateExpectedResult_StringScale("A"));

    public static Task<ITestData<ISyntacticScaledUnitInstance>> Constructor_String_String_Double => Lazy_Constructor_String_String_Double.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> Constructor_String_String_String_Double => Lazy_Constructor_String_String_String_Double.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> Constructor_String_String_String => Lazy_Constructor_String_String_String.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> Constructor_String_String_String_String => Lazy_Constructor_String_String_String_String.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> PluralForm_Null => Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> PluralForm_Empty => Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> PluralForm_String => Lazy_PluralForm_String.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> OriginalUnitInstance_Null => Lazy_OriginalUnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> OriginalUnitInstance_Empty => Lazy_OriginalUnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> OriginalUnitInstance_String => Lazy_OriginalUnitInstance_String.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> DoubleScale => Lazy_DoubleScale.Value;

    public static Task<ITestData<ISyntacticScaledUnitInstance>> StringScale_Null => Lazy_StringScale_Null.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> StringScale_Empty => Lazy_StringScale_Empty.Value;
    public static Task<ITestData<ISyntacticScaledUnitInstance>> StringScale_String => Lazy_StringScale_String.Value;

    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_Constructor_String_String_Double(string? name, string? originalUnitInstance, double scale)
    {
        var source = $$"""
            [SharpMeasures.ScaledUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{scale}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var scaleLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        ScaledUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, scaleLocation);

        SyntacticScaledUnitInstance expectedResult = new(name, originalUnitInstance, scale, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_Constructor_String_String_String_Double(string? name, string? pluralForm, string? originalUnitInstance, double scale)
    {
        var source = $$"""
            [SharpMeasures.ScaledUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{scale}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var scaleLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        ScaledUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, scaleLocation) { PluralForm = pluralFormLocation };

        SyntacticScaledUnitInstance expectedResult = new(name, originalUnitInstance, scale, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_Constructor_String_String_String(string? name, string? originalUnitInstance, string? scale)
    {
        var source = $$"""
            [SharpMeasures.ScaledUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.Create(scale)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var scaleLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        ScaledUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, scaleLocation);

        SyntacticScaledUnitInstance expectedResult = new(name, originalUnitInstance, scale, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_Constructor_String_String_String_String(string? name, string? pluralForm, string? originalUnitInstance, string? scale)
    {
        var source = $$"""
            [SharpMeasures.ScaledUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.Create(scale)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var scaleLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        ScaledUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, scaleLocation) { PluralForm = pluralFormLocation };

        SyntacticScaledUnitInstance expectedResult = new(name, originalUnitInstance, scale, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String_String(name, "A", "B");
    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String_String_Double("A", pluralForm, "B", 3.14);
    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_OriginalUnitInstance(string? originalUnitInstance) => await CreateExpectedResult_Constructor_String_String_String("A", originalUnitInstance, "B");
    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_DoubleScale(double scale) => await CreateExpectedResult_Constructor_String_String_Double("A", "B", scale);
    private static async Task<ITestData<ISyntacticScaledUnitInstance>> CreateExpectedResult_StringScale(string? scale) => await CreateExpectedResult_Constructor_String_String_String("A", "B", scale);

    private sealed class SyntacticScaledUnitInstance : ISyntacticScaledUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public string? OriginalUnitInstance { get; }

        public OneOf<double, string?> Scale { get; }

        public IScaledUnitInstanceSyntax Syntax { get; }

        public SyntacticScaledUnitInstance(string? name, string? originalUnitInstance, OneOf<double, string?> scale, IScaledUnitInstanceSyntax syntax)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Scale = scale;

            Syntax = syntax;
        }

        IModifiedUnitInstanceSyntax ISyntacticModifiedUnitInstance.Syntax => Syntax;
        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class ScaledUnitInstanceSyntax : AAttributeSyntax, IScaledUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public Location OriginalUnitInstance { get; }

        public Location Scale { get; }

        public ScaledUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location originalUnitInstance, Location scale) : base(attributeName, attribute)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Scale = scale;
        }
    }
}
