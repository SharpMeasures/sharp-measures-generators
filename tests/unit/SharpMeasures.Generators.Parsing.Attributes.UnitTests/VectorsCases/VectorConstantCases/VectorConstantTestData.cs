namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorConstantCases;

using Microsoft.CodeAnalysis;

using OneOf;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

internal static class VectorConstantTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_Constructor_String_String_DoubleCollection { get; } = new(() => CreateExpectedResult_Constructor_String_String_DoubleCollection("A", "B", Array.Empty<double>()));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_Constructor_String_String_StringCollection { get; } = new(() => CreateExpectedResult_Constructor_String_String_StringCollection("A", "B", Array.Empty<string?>()));

    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_UnitInstance_Null { get; } = new(() => CreateExpectedResult_UnitInstance(null));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_UnitInstance_Empty { get; } = new(() => CreateExpectedResult_UnitInstance(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_UnitInstance_String { get; } = new(() => CreateExpectedResult_UnitInstance("A"));

    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_DoubleCollection_Null { get; } = new(() => CreateExpectedResult_DoubleCollection(null));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_DoubleCollection_Empty { get; } = new(() => CreateExpectedResult_DoubleCollection(Array.Empty<double>()));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_DoubleCollection_Populated { get; } = new(() => CreateExpectedResult_DoubleCollection(new[] { 3.14 }));

    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_StringCollection_Null { get; } = new(() => CreateExpectedResult_StringCollection(null));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_StringCollection_Empty { get; } = new(() => CreateExpectedResult_StringCollection(Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticVectorConstant>>> Lazy_StringCollection_Populated { get; } = new(() => CreateExpectedResult_StringCollection(new[] { "A", string.Empty, null }));

    public static Task<ITestData<ISyntacticVectorConstant>> Constructor_String_String_DoubleCollection => Lazy_Constructor_String_String_DoubleCollection.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> Constructor_String_String_StringCollection => Lazy_Constructor_String_String_StringCollection.Value;

    public static Task<ITestData<ISyntacticVectorConstant>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticVectorConstant>> UnitInstance_Null => Lazy_UnitInstance_Null.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> UnitInstance_Empty => Lazy_UnitInstance_Empty.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> UnitInstance_String => Lazy_UnitInstance_String.Value;

    public static Task<ITestData<ISyntacticVectorConstant>> DoubleCollection_Null => Lazy_DoubleCollection_Null.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> DoubleCollection_Empty => Lazy_DoubleCollection_Empty.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> DoubleCollection_Populated => Lazy_DoubleCollection_Populated.Value;

    public static Task<ITestData<ISyntacticVectorConstant>> StringCollection_Null => Lazy_StringCollection_Null.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> StringCollection_Empty => Lazy_StringCollection_Empty.Value;
    public static Task<ITestData<ISyntacticVectorConstant>> StringCollection_Populated => Lazy_StringCollection_Populated.Value;

    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_Constructor_String_String_DoubleCollection(string? name, string? unitInstance, IReadOnlyList<double>? value)
    {
        var source = $$"""
            [SharpMeasures.VectorConstant({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(unitInstance)}}, {{StringRepresentationFactory.Create("double", value?.Select(static (value) => value.ToString(CultureInfo.InvariantCulture)))}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (valueCollectionLocation, valueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        VectorConstantSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstanceLocation, valueCollectionLocation, valueElementLocations);

        SyntacticVectorConstant expectedResult = new(name, unitInstance, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT0(value), syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_Constructor_String_String_StringCollection(string? name, string? unitInstance, IReadOnlyList<string?>? value)
    {
        var quotedValue = StringRepresentationFactory.Create("string", value?.Select(quoteValueElement));

        var source = $$"""
            [SharpMeasures.VectorConstant({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(unitInstance)}}, {{quotedValue}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var unitInstanceLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (valueCollectionLocation, valueElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        VectorConstantSyntax syntax = new(attributeNameLocation, attributeLocation, nameLocation, unitInstanceLocation, valueCollectionLocation, valueElementLocations);

        SyntacticVectorConstant expectedResult = new(name, unitInstance, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?>.FromT1(value), syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string quoteValueElement(string? valueElement) => valueElement switch
        {
            null => "null",
            not null => $"\"{valueElement}\""
        };
    }

    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_Name(string? name) => await CreateExpectedResult_Constructor_String_String_StringCollection(name, "A", Array.Empty<string?>());
    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_UnitInstance(string? unitInstance) => await CreateExpectedResult_Constructor_String_String_StringCollection("A", unitInstance, Array.Empty<string?>());
    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_DoubleCollection(IReadOnlyList<double>? value) => await CreateExpectedResult_Constructor_String_String_DoubleCollection("A", "B", value);
    private static async Task<ITestData<ISyntacticVectorConstant>> CreateExpectedResult_StringCollection(IReadOnlyList<string?>? value) => await CreateExpectedResult_Constructor_String_String_StringCollection("A", "B", value);

    private sealed class SyntacticVectorConstant : ISyntacticVectorConstant
    {
        public string? Name { get; }
        public string? UnitInstance { get; }

        public OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> Value { get; }

        public IVectorConstantSyntax Syntax { get; }

        public SyntacticVectorConstant(string? name, string? unitInstance, OneOf<IReadOnlyList<double>?, IReadOnlyList<string?>?> value, IVectorConstantSyntax syntax)
        {
            Name = name;
            UnitInstance = unitInstance;

            Value = value;

            Syntax = syntax;
        }
    }

    private sealed class VectorConstantSyntax : AAttributeSyntax, IVectorConstantSyntax
    {
        public Location Name { get; }
        public Location UnitInstance { get; }

        public Location ValueCollection { get; }
        public IReadOnlyList<Location> ValueElements { get; }

        public VectorConstantSyntax(Location attributeName, Location attribute, Location name, Location unitInstance, Location valueCollection, IReadOnlyList<Location> valueElements)
            : base(attributeName, attribute)
        {
            Name = name;
            UnitInstance = unitInstance;

            ValueCollection = valueCollection;
            ValueElements = valueElements;
        }
    }
}
