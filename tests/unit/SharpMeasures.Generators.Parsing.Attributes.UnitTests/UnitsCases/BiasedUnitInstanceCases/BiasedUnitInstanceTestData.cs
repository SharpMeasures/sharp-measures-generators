namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.BiasedUnitInstanceCases;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class BiasedUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Constructor_String_String_Double { get; } = new(() => CreateExpectedResult_Constructor_String_String_Double("A", "B", 3.14));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Constructor_String_String_String_Double { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_Double("A", "B", "C", 3.14));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Constructor_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String("A", "B", "C"));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Constructor_String_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_String("A", "B", "C", "D"));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_OriginalUnitInstance_Null { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_OriginalUnitInstance_Empty { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_OriginalUnitInstance_String { get; } = new(() => CreateExpectedResult_OriginalUnitInstance("A"));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_DoubleBias { get; } = new(() => CreateExpectedResult_DoubleBias(3.14));

    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_StringBias_Null { get; } = new(() => CreateExpectedResult_StringBias(null));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_StringBias_Empty { get; } = new(() => CreateExpectedResult_StringBias(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticBiasedUnitInstance>>> Lazy_StringBias_String { get; } = new(() => CreateExpectedResult_StringBias("A"));

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Constructor_String_String_Double => Lazy_Constructor_String_String_Double.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Constructor_String_String_String_Double => Lazy_Constructor_String_String_String_Double.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Constructor_String_String_String => Lazy_Constructor_String_String_String.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Constructor_String_String_String_String => Lazy_Constructor_String_String_String_String.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> PluralForm_Null => Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> PluralForm_Empty => Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> PluralForm_String => Lazy_PluralForm_String.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> OriginalUnitInstance_Null => Lazy_OriginalUnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> OriginalUnitInstance_Empty => Lazy_OriginalUnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> OriginalUnitInstance_String => Lazy_OriginalUnitInstance_String.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> DoubleBias => Lazy_DoubleBias.Value;

    public static Task<ITestData<ISyntacticBiasedUnitInstance>> StringBias_Null => Lazy_StringBias_Null.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> StringBias_Empty => Lazy_StringBias_Empty.Value;
    public static Task<ITestData<ISyntacticBiasedUnitInstance>> StringBias_String => Lazy_StringBias_String.Value;

    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_Constructor_String_String_Double(string? name, string? originalUnitInstance, double bias)
    {
        var source = $$"""
            [SharpMeasures.BiasedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{bias}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        BiasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation);

        SyntacticBiasedUnitInstance expectedResult = new(name, originalUnitInstance, bias, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_Constructor_String_String_String_Double(string? name, string? pluralForm, string? originalUnitInstance, double bias)
    {
        var source = $$"""
            [SharpMeasures.BiasedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{bias}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        BiasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation) { PluralForm = pluralFormLocation };

        SyntacticBiasedUnitInstance expectedResult = new(name, originalUnitInstance, bias, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_Constructor_String_String_String(string? name, string? originalUnitInstance, string? bias)
    {
        var source = $$"""
            [SharpMeasures.BiasedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.Create(bias)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        BiasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation);

        SyntacticBiasedUnitInstance expectedResult = new(name, originalUnitInstance, bias, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_Constructor_String_String_String_String(string? name, string? pluralForm, string? originalUnitInstance, string? bias)
    {
        var source = $$"""
            [SharpMeasures.BiasedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.Create(bias)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        BiasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation) { PluralForm = pluralFormLocation };

        SyntacticBiasedUnitInstance expectedResult = new(name, originalUnitInstance, bias, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String_String(name, "A", "B");
    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String_String_Double("A", pluralForm, "B", 3.14);
    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_OriginalUnitInstance(string? originalUnitInstance) => await CreateExpectedResult_Constructor_String_String_String("A", originalUnitInstance, "B");
    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_DoubleBias(double bias) => await CreateExpectedResult_Constructor_String_String_Double("A", "B", bias);
    private static async Task<ITestData<ISyntacticBiasedUnitInstance>> CreateExpectedResult_StringBias(string? bias) => await CreateExpectedResult_Constructor_String_String_String("A", "B", bias);

    private sealed class SyntacticBiasedUnitInstance : ISyntacticBiasedUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public string? OriginalUnitInstance { get; }

        public OneOf<double, string?> Bias { get; }

        public IBiasedUnitInstanceSyntax Syntax { get; }

        public SyntacticBiasedUnitInstance(string? name, string? originalUnitInstance, OneOf<double, string?> bias, IBiasedUnitInstanceSyntax syntax)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Bias = bias;

            Syntax = syntax;
        }

        IModifiedUnitInstanceSyntax ISyntacticModifiedUnitInstance.Syntax => Syntax;
        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class BiasedUnitInstanceSyntax : AAttributeSyntax, IBiasedUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public Location OriginalUnitInstance { get; }

        public Location Bias { get; }

        public BiasedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location originalUnitInstance, Location bias) : base(attributeName, attribute)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Bias = bias;
        }
    }
}
