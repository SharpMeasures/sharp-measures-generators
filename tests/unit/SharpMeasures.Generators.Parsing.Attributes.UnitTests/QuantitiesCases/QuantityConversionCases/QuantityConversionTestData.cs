namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityConversionCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal static class QuantityConversionTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_Constructor_TypeCollection { get; } = new(CreateExpectedResult_Constructor_TypeCollection);

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_Quantities_Null { get; } = new(CreateExpectedResult_Quantities_Null);
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_Quantities_Empty { get; } = new(CreateExpectedResult_Quantities_Empty);
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_Quantities_Populated { get; } = new(CreateExpectedResult_Quantities_Populated);

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsImplementation_Unrecognized { get; } = new(() => CreateExpectedResult_ForwardsImplementation((ConversionImplementation)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsImplementation_Recognized { get; } = new(() => CreateExpectedResult_ForwardsImplementation(ConversionImplementation.InstanceMethod));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsBehaviour_Unrecognized { get; } = new(() => CreateExpectedResult_ForwardsBehaviour((ConversionOperatorBehaviour)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsBehaviour_Recognized { get; } = new(() => CreateExpectedResult_ForwardsBehaviour(ConversionOperatorBehaviour.Implicit));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsPropertyName_Null { get; } = new(() => CreateExpectedResult_ForwardsPropertyName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsPropertyName_Empty { get; } = new(() => CreateExpectedResult_ForwardsPropertyName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsPropertyName_String { get; } = new(() => CreateExpectedResult_ForwardsPropertyName("ForwardsPropertyName"));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsMethodName_Null { get; } = new(() => CreateExpectedResult_ForwardsMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsMethodName_Empty { get; } = new(() => CreateExpectedResult_ForwardsMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsMethodName_String { get; } = new(() => CreateExpectedResult_ForwardsMethodName("ForwardsMethodName"));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsStaticMethodName_Null { get; } = new(() => CreateExpectedResult_ForwardsStaticMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsStaticMethodName_Empty { get; } = new(() => CreateExpectedResult_ForwardsStaticMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_ForwardsStaticMethodName_String { get; } = new(() => CreateExpectedResult_ForwardsStaticMethodName("ForwardsStaticMethodName"));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsImplementation_Unrecognized { get; } = new(() => CreateExpectedResult_BackwardsImplementation((ConversionImplementation)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsImplementation_Recognized { get; } = new(() => CreateExpectedResult_BackwardsImplementation(ConversionImplementation.InstanceMethod));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsBehaviour_Unrecognized { get; } = new(() => CreateExpectedResult_BackwardsBehaviour((ConversionOperatorBehaviour)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsBehaviour_Recognized { get; } = new(() => CreateExpectedResult_BackwardsBehaviour(ConversionOperatorBehaviour.Implicit));

    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsStaticMethodName_Null { get; } = new(() => CreateExpectedResult_BackwardsStaticMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsStaticMethodName_Empty { get; } = new(() => CreateExpectedResult_BackwardsStaticMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityConversion>>> Lazy_BackwardsStaticMethodName_String { get; } = new(() => CreateExpectedResult_BackwardsStaticMethodName("BackwardsStaticMethodName"));

    public static Task<ITestData<ISyntacticQuantityConversion>> Constructor_Empty => Lazy_Constructor_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> Constructor_TypeCollection => Lazy_Constructor_TypeCollection.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> Quantities_Null => Lazy_Quantities_Null.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> Quantities_Empty => Lazy_Quantities_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> Quantities_Populated => Lazy_Quantities_Populated.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsImplementation_Unrecognized => Lazy_ForwardsImplementation_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsImplementation_Recognized => Lazy_ForwardsImplementation_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsBehaviour_Unrecognized => Lazy_ForwardsBehaviour_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsBehaviour_Recognized => Lazy_ForwardsBehaviour_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsPropertyName_Null => Lazy_ForwardsPropertyName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsPropertyName_Empty => Lazy_ForwardsPropertyName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsPropertyName_String => Lazy_ForwardsPropertyName_String.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsMethodName_Null => Lazy_ForwardsMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsMethodName_Empty => Lazy_ForwardsMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsMethodName_String => Lazy_ForwardsMethodName_String.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsStaticMethodName_Null => Lazy_ForwardsStaticMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsStaticMethodName_Empty => Lazy_ForwardsStaticMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> ForwardsStaticMethodName_String => Lazy_ForwardsStaticMethodName_String.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsImplementation_Unrecognized => Lazy_BackwardsImplementation_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsImplementation_Recognized => Lazy_BackwardsImplementation_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsBehaviour_Unrecognized => Lazy_BackwardsBehaviour_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsBehaviour_Recognized => Lazy_BackwardsBehaviour_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsStaticMethodName_Null => Lazy_BackwardsStaticMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsStaticMethodName_Empty => Lazy_BackwardsStaticMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityConversion>> BackwardsStaticMethodName_String => Lazy_BackwardsStaticMethodName_String.Value;

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Constructor_Empty()
    {
        var source = $$"""
            [SharpMeasures.QuantityConversion]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        QuantityConversionSyntax syntax = new(attributeNameLocation, attributeLocation, Location.None, Array.Empty<Location>());

        SyntacticQuantityConversion expectedResult = new(Array.Empty<ITypeSymbol>(), syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Constructor(IReadOnlyList<string?>? quantities, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> quantitiesSymbols)
    {
        var typeofQuantities = StringRepresentationFactory.Create("System.Type", quantities?.Select(typeofQuantity).ToList());

        var source = $$"""
            [SharpMeasures.QuantityConversion({{typeofQuantities}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var (quantitiesCollectionLocation, quantitiesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        QuantityConversionSyntax syntax = new(attributeNameLocation, attributeLocation, quantitiesCollectionLocation, quantitiesElementLocations);

        SyntacticQuantityConversion expectedResult = new(quantitiesSymbols(compilation), syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string typeofQuantity(string? quantity) => quantity switch
        {
            null => "null",
            not null => $"typeof({quantity})"
        };
    }

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Constructor_TypeCollection()
    {
        return await CreateExpectedResult_Constructor(new[] { "string", "int", null }, quantitiesSymbols);

        static IReadOnlyList<ITypeSymbol?> quantitiesSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Quantities_Null() => await CreateExpectedResult_Quantities(null, static (compilation) => null);
    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Quantities_Empty() => await CreateExpectedResult_Quantities(Array.Empty<string?>(), static (compilation) => Array.Empty<ITypeSymbol?>());
    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Quantities_Populated() => await CreateExpectedResult_Constructor_TypeCollection();
    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_Quantities(IReadOnlyList<string?>? quantities, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> quantitiesSymbols) => await CreateExpectedResult_Constructor(quantities, quantitiesSymbols);

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_ForwardsImplementation(ConversionImplementation forwardsImplementation) => await CreateExpectedResult_SingleProperty
    (
        "ForwardsImplementation",
        StringRepresentationFactory.CreateEnum(forwardsImplementation),
        (quantityConversion) => quantityConversion.ForwardsImplementation = forwardsImplementation,
        (quantityConversionSyntax, location) => quantityConversionSyntax.ForwardsImplementation = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_ForwardsBehaviour(ConversionOperatorBehaviour forwardsBehaviour) => await CreateExpectedResult_SingleProperty
    (
        "ForwardsBehaviour",
        StringRepresentationFactory.CreateEnum(forwardsBehaviour),
        (quantityConversion) => quantityConversion.ForwardsBehaviour = forwardsBehaviour,
        (quantityConversionSyntax, location) => quantityConversionSyntax.ForwardsBehaviour = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_ForwardsPropertyName(string? forwardsPropertyName) => await CreateExpectedResult_SingleProperty
    (
        "ForwardsPropertyName",
        StringRepresentationFactory.Create(forwardsPropertyName),
        (quantityConversion) => quantityConversion.ForwardsPropertyName = forwardsPropertyName,
        (quantityConversionSyntax, location) => quantityConversionSyntax.ForwardsPropertyName = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_ForwardsMethodName(string? forwardsMethodName) => await CreateExpectedResult_SingleProperty
    (
        "ForwardsMethodName",
        StringRepresentationFactory.Create(forwardsMethodName),
        (quantityConversion) => quantityConversion.ForwardsMethodName = forwardsMethodName,
        (quantityConversionSyntax, location) => quantityConversionSyntax.ForwardsMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_ForwardsStaticMethodName(string? forwardsStaticMethodName) => await CreateExpectedResult_SingleProperty
    (
        "ForwardsStaticMethodName",
        StringRepresentationFactory.Create(forwardsStaticMethodName),
        (quantityConversion) => quantityConversion.ForwardsStaticMethodName = forwardsStaticMethodName,
        (quantityConversionSyntax, location) => quantityConversionSyntax.ForwardsStaticMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_BackwardsImplementation(ConversionImplementation backwardsImplementation) => await CreateExpectedResult_SingleProperty
    (
        "BackwardsImplementation",
        StringRepresentationFactory.CreateEnum(backwardsImplementation),
        (quantityConversion) => quantityConversion.BackwardsImplementation = backwardsImplementation,
        (quantityConversionSyntax, location) => quantityConversionSyntax.BackwardsImplementation = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_BackwardsBehaviour(ConversionOperatorBehaviour backwardsBehaviour) => await CreateExpectedResult_SingleProperty
    (
        "BackwardsBehaviour",
        StringRepresentationFactory.CreateEnum(backwardsBehaviour),
        (quantityConversion) => quantityConversion.BackwardsBehaviour = backwardsBehaviour,
        (quantityConversionSyntax, location) => quantityConversionSyntax.BackwardsBehaviour = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_BackwardsStaticMethodName(string? backwardsStaticMethodName) => await CreateExpectedResult_SingleProperty
    (
        "BackwardsStaticMethodName",
        StringRepresentationFactory.Create(backwardsStaticMethodName),
        (quantityConversion) => quantityConversion.BackwardsStaticMethodName = backwardsStaticMethodName,
        (quantityConversionSyntax, location) => quantityConversionSyntax.BackwardsStaticMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityConversion>> CreateExpectedResult_SingleProperty(string propertyName, string stringRepresentation, Action<SyntacticQuantityConversion> setter, Action<QuantityConversionSyntax, Location> syntaxSetter)
    {
        var source = $$"""
            [SharpMeasures.QuantityConversion({{propertyName}} = {{stringRepresentation}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var otherPropertyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        QuantityConversionSyntax syntax = new(attributeNameLocation, attributeLocation, Location.None, Array.Empty<Location>());

        SyntacticQuantityConversion expectedResult = new(Array.Empty<ITypeSymbol>(), syntax);

        syntaxSetter(syntax, otherPropertyLocation);
        setter(expectedResult);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticQuantityConversion : ISyntacticQuantityConversion
    {
        public IReadOnlyList<ITypeSymbol?>? Quantities { get; }

        public ConversionImplementation? ForwardsImplementation { get; set; }
        public ConversionOperatorBehaviour? ForwardsBehaviour { get; set; }
        public string? ForwardsPropertyName { get; set; }
        public string? ForwardsMethodName { get; set; }
        public string? ForwardsStaticMethodName { get; set; }

        public ConversionImplementation? BackwardsImplementation { get; set; }
        public ConversionOperatorBehaviour? BackwardsBehaviour { get; set; }
        public string? BackwardsStaticMethodName { get; set; }

        public IQuantityConversionSyntax Syntax { get; }

        public SyntacticQuantityConversion(IReadOnlyList<ITypeSymbol?>? quantities, IQuantityConversionSyntax syntax)
        {
            Quantities = quantities;

            Syntax = syntax;
        }
    }

    private sealed class QuantityConversionSyntax : AAttributeSyntax, IQuantityConversionSyntax
    {
        public Location QuantitiesCollection { get; }
        public IReadOnlyList<Location> QuantitiesElements { get; }

        public Location ForwardsImplementation { get; set; } = Location.None;
        public Location ForwardsBehaviour { get; set; } = Location.None;
        public Location ForwardsPropertyName { get; set; } = Location.None;
        public Location ForwardsMethodName { get; set; } = Location.None;
        public Location ForwardsStaticMethodName { get; set; } = Location.None;

        public Location BackwardsImplementation { get; set; } = Location.None;
        public Location BackwardsBehaviour { get; set; } = Location.None;
        public Location BackwardsStaticMethodName { get; set; } = Location.None;

        public QuantityConversionSyntax(Location attributeName, Location attribute, Location quantitiesCollection, IReadOnlyList<Location> quantitiesElements) : base(attributeName, attribute)
        {
            QuantitiesCollection = quantitiesCollection;
            QuantitiesElements = quantitiesElements;
        }
    }
}
