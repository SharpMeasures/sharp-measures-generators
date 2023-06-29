namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.FixedUnitInstanceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Threading.Tasks;

internal static class FixedUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_Constructor_String { get; } = new(() => CreateExpectedResult_Constructor_String("A"));
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_Constructor_String_String { get; } = new(() => CreateExpectedResult_Constructor_String_String("A", "B"));

    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticFixedUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    public static Task<ITestData<ISyntacticFixedUnitInstance>> Constructor_String => Lazy_Constructor_String.Value;
    public static Task<ITestData<ISyntacticFixedUnitInstance>> Constructor_String_String => Lazy_Constructor_String_String.Value;

    public static Task<ITestData<ISyntacticFixedUnitInstance>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticFixedUnitInstance>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticFixedUnitInstance>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticFixedUnitInstance>> PluralForm_Null => Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticFixedUnitInstance>> PluralForm_Empty => Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticFixedUnitInstance>> PluralForm_String => Lazy_PluralForm_String.Value;

    private static async Task<ITestData<ISyntacticFixedUnitInstance>> CreateExpectedResult_Constructor_String(string? name)
    {
        var source = $$"""
            [SharpMeasures.FixedUnitInstance({{StringRepresentationFactory.Create(name)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        FixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation);

        SyntacticFixedUnitInstance expectedResult = new(name, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticFixedUnitInstance>> CreateExpectedResult_Constructor_String_String(string? name, string? pluralForm)
    {
        var source = $$"""
            [SharpMeasures.FixedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        FixedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation) { PluralForm = pluralFormLocation };

        SyntacticFixedUnitInstance expectedResult = new(name, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticFixedUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String(name);
    private static async Task<ITestData<ISyntacticFixedUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String("A", pluralForm);

    private sealed class SyntacticFixedUnitInstance : ISyntacticFixedUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public IFixedUnitInstanceSyntax Syntax { get; }

        public SyntacticFixedUnitInstance(string? name, IFixedUnitInstanceSyntax syntax)
        {
            Name = name;

            Syntax = syntax;
        }

        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class FixedUnitInstanceSyntax : AAttributeSyntax, IFixedUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public FixedUnitInstanceSyntax(Location attributeName, Location attribute, Location name) : base(attributeName, attribute)
        {
            Name = name;
        }
    }
}
