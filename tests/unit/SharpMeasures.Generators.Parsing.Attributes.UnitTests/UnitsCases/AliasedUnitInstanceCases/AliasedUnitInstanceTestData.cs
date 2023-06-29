namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.AliasedUnitInstanceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class AliasedUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_Constructor_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String("A", "B"));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_Constructor_String_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String_String("A", "B", "C"));

    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_OriginalUnitInstance_Null { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_OriginalUnitInstance_Empty { get; } = new(() => CreateExpectedResult_OriginalUnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticAliasedUnitInstance>>> Lazy_OriginalUnitInstance_String { get; } = new(() => CreateExpectedResult_OriginalUnitInstance("A"));

    public static Task<ITestData<ISyntacticAliasedUnitInstance>> Constructor_String_String = Lazy_Constructor_String_String.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> Constructor_String_String_String = Lazy_Constructor_String_String_String.Value;

    public static Task<ITestData<ISyntacticAliasedUnitInstance>> Name_Null = Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> Name_Empty = Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> Name_String = Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticAliasedUnitInstance>> PluralForm_Null = Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> PluralForm_Empty = Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> PluralForm_String = Lazy_PluralForm_String.Value;

    public static Task<ITestData<ISyntacticAliasedUnitInstance>> OriginalUnitInstance_Null = Lazy_OriginalUnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> OriginalUnitInstance_Empty = Lazy_OriginalUnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticAliasedUnitInstance>> OriginalUnitInstance_String = Lazy_OriginalUnitInstance_String.Value;

    private static async Task<ITestData<ISyntacticAliasedUnitInstance>> CreateExpectedResult_Constructor_String_String(string? name, string? originalUnitInstance)
    {
        var source = $$"""
            [SharpMeasures.UnitInstanceAlias({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        AliasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation);

        SyntacticAliasedUnitInstance expectedResult = new(name, originalUnitInstance, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticAliasedUnitInstance>> CreateExpectedResult_Constructor_String_String_String(string? name, string? pluralForm, string? originalUnitInstance)
    {
        var source = $$"""
            [SharpMeasures.UnitInstanceAlias({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(originalUnitInstance)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var originalUnitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        AliasedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, originalUnitInstanceLocation) { PluralForm = pluralFormLocation };

        SyntacticAliasedUnitInstance expectedResult = new(name, originalUnitInstance, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticAliasedUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String(name, "A");
    private static async Task<ITestData<ISyntacticAliasedUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String_String("A", pluralForm, "B");
    private static async Task<ITestData<ISyntacticAliasedUnitInstance>> CreateExpectedResult_OriginalUnitInstance(string? originalUnitInstance) => await CreateExpectedResult_Constructor_String_String("A", originalUnitInstance);

    private sealed class SyntacticAliasedUnitInstance : ISyntacticAliasedUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public string? OriginalUnitInstance { get; }

        public IAliasedUnitInstanceSyntax Syntax { get; }

        public SyntacticAliasedUnitInstance(string? name, string? originalUnitInstance, IAliasedUnitInstanceSyntax syntax)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;

            Syntax = syntax;
        }

        IModifiedUnitInstanceSyntax ISyntacticModifiedUnitInstance.Syntax => Syntax;
        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class AliasedUnitInstanceSyntax : AAttributeSyntax, IAliasedUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public Location OriginalUnitInstance { get; }

        public AliasedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location originalUnitInstance) : base(attributeName, attribute)
        {
            Name = name;

            OriginalUnitInstance = originalUnitInstance;
        }
    }
}
