namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.DerivedUnitInstanceCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal static class DerivedUnitInstanceTestData
{
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Constructor_String_String_String_StringCollection { get; } = new(() => CreateExpectedResult_Constructor_String_String_String_StringCollection("A", "B", "C", Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Constructor_String_String_StringCollection { get; } = new(() => CreateExpectedResult_Constructor_String_String_StringCollection("A", "B", Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Constructor_String_StringCollection { get; } = new(() => CreateExpectedResult_Constructor_String_StringCollection("A", Array.Empty<string?>()));

    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_PluralForm_Null { get; } = new(() => CreateExpectedResult_PluralForm(null));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_PluralForm_Empty { get; } = new(() => CreateExpectedResult_PluralForm(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_PluralForm_String { get; } = new(() => CreateExpectedResult_PluralForm("A"));

    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_DerivationID_Null { get; } = new(() => CreateExpectedResult_DerivationID(null));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_DerivationID_Empty { get; } = new(() => CreateExpectedResult_DerivationID(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_DerivationID_String { get; } = new(() => CreateExpectedResult_DerivationID("A"));

    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_UnitInstances_Null { get; } = new(() => CreateExpectedResult_UnitInstances(null));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_UnitInstances_Empty { get; } = new(() => CreateExpectedResult_UnitInstances(Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticDerivedUnitInstance>>> Lazy_UnitInstances_Populated { get; } = new(() => CreateExpectedResult_UnitInstances(new[] { "A", string.Empty, null }));

    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Constructor_String_String_String_StringCollection => Lazy_Constructor_String_String_String_StringCollection.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Constructor_String_String_StringCollection => Lazy_Constructor_String_String_StringCollection.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Constructor_String_StringCollection => Lazy_Constructor_String_StringCollection.Value;

    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticDerivedUnitInstance>> PluralForm_Null => Lazy_PluralForm_Null.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> PluralForm_Empty => Lazy_PluralForm_Empty.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> PluralForm_String => Lazy_PluralForm_String.Value;

    public static Task<ITestData<ISyntacticDerivedUnitInstance>> DerivationID_Null => Lazy_DerivationID_Null.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> DerivationID_Empty => Lazy_DerivationID_Empty.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> DerivationID_String => Lazy_DerivationID_String.Value;

    public static Task<ITestData<ISyntacticDerivedUnitInstance>> UnitInstances_Null => Lazy_UnitInstances_Null.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> UnitInstances_Empty => Lazy_UnitInstances_Empty.Value;
    public static Task<ITestData<ISyntacticDerivedUnitInstance>> UnitInstances_Populated => Lazy_UnitInstances_Populated.Value;

    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_Constructor_String_String_String_StringCollection(string? name, string? pluralForm, string? derivationID, IReadOnlyList<string?>? unitInstances)
    {
        var quotedUnitInstances = StringRepresentationFactory.Create("string", unitInstances?.Select(quoteUnitInstances));

        var source = $$"""
            [SharpMeasures.DerivedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{StringRepresentationFactory.Create(derivationID)}}, {{quotedUnitInstances}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var derivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);
        var (unitInstancesCollectionLocation, unitInstancesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);

        DerivedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstancesCollectionLocation, unitInstancesElementLocations)
        {
            PluralForm = pluralFormLocation,
            DerivationID = derivationIDLocation
        };

        SyntacticDerivedUnitInstance expectedResult = new(name, unitInstances, syntax)
        {
            PluralForm = pluralForm,
            DerivationID = derivationID
        };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string quoteUnitInstances(string? unitInstance) => unitInstance switch
        {
            null => "null",
            not null => $"\"{unitInstance}\""
        };
    }

    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_Constructor_String_String_StringCollection(string? name, string? pluralForm, IReadOnlyList<string?>? unitInstances)
    {
        var quotedUnitInstances = StringRepresentationFactory.Create("string", unitInstances?.Select(quoteUnitInstances));

        var source = $$"""
            [SharpMeasures.DerivedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(pluralForm)}}, {{quotedUnitInstances}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var pluralFormLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (unitInstancesCollectionLocation, unitInstancesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        DerivedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstancesCollectionLocation, unitInstancesElementLocations) { PluralForm = pluralFormLocation };

        SyntacticDerivedUnitInstance expectedResult = new(name, unitInstances, syntax) { PluralForm = pluralForm };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string quoteUnitInstances(string? unitInstance) => unitInstance switch
        {
            null => "null",
            not null => $"\"{unitInstance}\""
        };
    }

    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_Constructor_String_StringCollection(string? name, IReadOnlyList<string?>? unitInstances)
    {
        var quotedUnitInstances = StringRepresentationFactory.Create("string", unitInstances?.Select(quoteUnitInstances));

        var source = $$"""
            [SharpMeasures.DerivedUnitInstance({{StringRepresentationFactory.Create(name)}}, {{quotedUnitInstances}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (unitInstancesCollectionLocation, unitInstancesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        DerivedUnitInstanceSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstancesCollectionLocation, unitInstancesElementLocations);

        SyntacticDerivedUnitInstance expectedResult = new(name, unitInstances, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string quoteUnitInstances(string? unitInstance) => unitInstance switch
        {
            null => "null",
            not null => $"\"{unitInstance}\""
        };
    }

    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_StringCollection(name, Array.Empty<string?>());
    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_PluralForm(string? pluralForm) => await CreateExpectedResult_Constructor_String_String_StringCollection("A", pluralForm, Array.Empty<string?>());
    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_DerivationID(string? derivationID) => await CreateExpectedResult_Constructor_String_String_String_StringCollection("A", "B", derivationID, Array.Empty<string?>());
    private static async Task<ITestData<ISyntacticDerivedUnitInstance>> CreateExpectedResult_UnitInstances(IReadOnlyList<string?>? unitInstances) => await CreateExpectedResult_Constructor_String_StringCollection("A", unitInstances);

    private sealed class SyntacticDerivedUnitInstance : ISyntacticDerivedUnitInstance
    {
        public string? Name { get; }
        public string? PluralForm { get; init; }

        public string? DerivationID { get; init; }

        public IReadOnlyList<string?>? UnitInstances { get; }

        public IDerivedUnitInstanceSyntax Syntax { get; }

        public SyntacticDerivedUnitInstance(string? name, IReadOnlyList<string?>? unitInstances, IDerivedUnitInstanceSyntax syntax)
        {
            Name = name;

            UnitInstances = unitInstances;

            Syntax = syntax;
        }

        IUnitInstanceSyntax ISyntacticUnitInstance.Syntax => Syntax;
    }

    private sealed class DerivedUnitInstanceSyntax : AAttributeSyntax, IDerivedUnitInstanceSyntax
    {
        public Location Name { get; }
        public Location PluralForm { get; init; } = Location.None;

        public Location DerivationID { get; init; } = Location.None;

        public Location UnitInstancesCollection { get; }
        public IReadOnlyList<Location> UnitInstancesElements { get; }

        public DerivedUnitInstanceSyntax(Location attributeName, Location attribute, Location name, Location unitInstancesCollection, IReadOnlyList<Location> unitInstancesElements)
            : base(attributeName, attribute)
        {
            Name = name;

            UnitInstancesCollection = unitInstancesCollection;
            UnitInstancesElements = unitInstancesElements;
        }
    }
}
