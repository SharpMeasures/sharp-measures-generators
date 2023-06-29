namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.PrefixedUnitInstanceCases;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class PrefixedUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Constructor_String_String_MetricPrefixName { get; } = new(() => CreateExpectedResult_Constructor_String_String_MetricPrefixName("A", "B", MetricPrefixName.Quetta));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Constructor_String_String_String_MetricPrefixName { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_MetricPrefixName("A", "B", "C", MetricPrefixName.Quetta));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Constructor_String_String_BinaryPrefixName { get; } = new(() => CreateExpectedResult_Constructor_String_String_BinaryPrefixName("A", "B", BinaryPrefixName.Zebi));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Constructor_String_String_String_BinaryPrefixName { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_BinaryPrefixName("A", "B", "C", BinaryPrefixName.Zebi));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_OriginalUnitInstance_Null { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_OriginalUnitInstance_Empty { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_OriginalUnitInstance_String { get; } = new(() => CreateExpectedResult_OriginalUnitInstance("A"));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_MetricPrefix_Unrecognized { get; } = new(() => CreateExpectedResult_MetricPrefix((MetricPrefixName)(-1)));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_MetricPrefix_Recognized { get; } = new(() => CreateExpectedResult_MetricPrefix(MetricPrefixName.Quetta));

    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_BinaryPrefix_Unrecognized { get; } = new(() => CreateExpectedResult_BinaryPrefix((BinaryPrefixName)(-1)));
    private static Lazy<Task<ITestData<ISyntacticPrefixedUnitInstance>>> Lazy_BinaryPrefix_Recognized { get; } = new(() => CreateExpectedResult_BinaryPrefix(BinaryPrefixName.Zebi));

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Constructor_String_String_MetricPrefixName => Lazy_Constructor_String_String_MetricPrefixName.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Constructor_String_String_String_MetricPrefixName => Lazy_Constructor_String_String_String_MetricPrefixName.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Constructor_String_String_BinaryPrefixName => Lazy_Constructor_String_String_BinaryPrefixName.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Constructor_String_String_String_BinaryPrefixName => Lazy_Constructor_String_String_String_BinaryPrefixName.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> PluralForm_Null => Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> PluralForm_Empty => Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> PluralForm_String => Lazy_PluralForm_String.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> OriginalUnitInstance_Null => Lazy_OriginalUnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> OriginalUnitInstance_Empty => Lazy_OriginalUnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> OriginalUnitInstance_String => Lazy_OriginalUnitInstance_String.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> MetricPrefix_Unrecognized => Lazy_MetricPrefix_Unrecognized.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> MetricPrefix_Recognized => Lazy_MetricPrefix_Recognized.Value;

    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> BinaryPrefix_Unrecognized => Lazy_BinaryPrefix_Unrecognized.Value;
    public static Task<ITestData<ISyntacticPrefixedUnitInstance>> BinaryPrefix_Recognized => Lazy_BinaryPrefix_Recognized.Value;

    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_Constructor_String_String_MetricPrefixName(string? name, string? originalUnitInstance, MetricPrefixName prefix)
    {
        var source = $$"""
            [SharpMeasures.PrefixedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.CreateEnum(prefix)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        PrefixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation);

        SyntacticPrefixedUnitInstance expectedResult = new(name, originalUnitInstance, prefix, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_Constructor_String_String_String_MetricPrefixName(string? name, string? pluralForm, string? originalUnitInstance, MetricPrefixName prefix)
    {
        var source = $$"""
            [SharpMeasures.PrefixedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.CreateEnum(prefix)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        PrefixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation) { PluralForm = pluralFormLocation };

        SyntacticPrefixedUnitInstance expectedResult = new(name, originalUnitInstance, prefix, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_Constructor_String_String_BinaryPrefixName(string? name, string? originalUnitInstance, BinaryPrefixName prefix)
    {
        var source = $$"""
            [SharpMeasures.PrefixedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.CreateEnum(prefix)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        PrefixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation);

        SyntacticPrefixedUnitInstance expectedResult = new(name, originalUnitInstance, prefix, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_Constructor_String_String_String_BinaryPrefixName(string? name, string? pluralForm, string? originalUnitInstance, BinaryPrefixName prefix)
    {
        var source = $$"""
            [SharpMeasures.PrefixedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}}, {{StringRepresentationFactory.CreateEnum(prefix)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var biasLocation = ExpectedLocation.SingleArgument(attributeSyntax, 3);

        PrefixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation, biasLocation) { PluralForm = pluralFormLocation };

        SyntacticPrefixedUnitInstance expectedResult = new(name, originalUnitInstance, prefix, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String_MetricPrefixName(name, "A", MetricPrefixName.Quetta);
    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String_String_MetricPrefixName("A", pluralForm, "B", MetricPrefixName.Quetta);
    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_OriginalUnitInstance(string? originalUnitInstance) => await CreateExpectedResult_Constructor_String_String_MetricPrefixName("A", originalUnitInstance, MetricPrefixName.Quetta);
    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_MetricPrefix(MetricPrefixName prefix) => await CreateExpectedResult_Constructor_String_String_MetricPrefixName("A", "B", prefix);
    private static async Task<ITestData<ISyntacticPrefixedUnitInstance>> CreateExpectedResult_BinaryPrefix(BinaryPrefixName prefix) => await CreateExpectedResult_Constructor_String_String_BinaryPrefixName("A", "B", prefix);

    private sealed class SyntacticPrefixedUnitInstance : ISyntacticPrefixedUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public string? OriginalUnitInstance { get; }

        public OneOf<MetricPrefixName, BinaryPrefixName> Prefix { get; }

        public IPrefixedUnitInstanceSyntax Syntax { get; }

        public SyntacticPrefixedUnitInstance(string? name, string? originalUnitInstance, OneOf<MetricPrefixName, BinaryPrefixName> prefix, IPrefixedUnitInstanceSyntax syntax)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Prefix = prefix;

            Syntax = syntax;
        }

        IModifiedUnitInstanceSyntax ISyntacticModifiedUnitInstance.Syntax => Syntax;
        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class PrefixedUnitInstanceSyntax : AAttributeSyntax, IPrefixedUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public Location OriginalUnitInstance { get; }

        public Location Prefix { get; }

        public PrefixedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location originalUnitInstance, Location prefix) : base(attributeName, attribute)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Prefix = prefix;
        }
    }
}
