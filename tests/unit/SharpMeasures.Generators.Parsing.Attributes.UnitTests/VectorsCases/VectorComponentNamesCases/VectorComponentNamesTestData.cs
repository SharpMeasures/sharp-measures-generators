namespace SharpMeasures.Generators.Parsing.Attributes.VectorsCases.VectorComponentNamesCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Vectors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal static class VectorComponentNamesTestData
{
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Constructor_StringCollection { get; } = new(() => CreateExpectedResult_Constructor_StringCollection(Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Constructor_String { get; } = new(() => CreateExpectedResult_Constructor_String("A"));

    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Names_Null { get; } = new(() => CreateExpectedResult_Names(null));
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Names_Empty { get; } = new(() => CreateExpectedResult_Names(Array.Empty<string?>()));
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Names_Populated { get; } = new(() => CreateExpectedResult_Names(new[] { "A", string.Empty, null }));

    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Expression_Null { get; } = new(() => CreateExpectedResult_Expression(null));
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Expression_Empty { get; } = new(() => CreateExpectedResult_Expression(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticVectorComponentNames>>> Lazy_Expression_String { get; } = new(() => CreateExpectedResult_Expression("A"));

    public static Task<ITestData<ISyntacticVectorComponentNames>> Constructor_StringCollection => Lazy_Constructor_StringCollection.Value;
    public static Task<ITestData<ISyntacticVectorComponentNames>> Constructor_String => Lazy_Constructor_String.Value;

    public static Task<ITestData<ISyntacticVectorComponentNames>> Names_Null => Lazy_Names_Null.Value;
    public static Task<ITestData<ISyntacticVectorComponentNames>> Names_Empty => Lazy_Names_Empty.Value;
    public static Task<ITestData<ISyntacticVectorComponentNames>> Names_Populated => Lazy_Names_Populated.Value;

    public static Task<ITestData<ISyntacticVectorComponentNames>> Expression_Null => Lazy_Expression_Null.Value;
    public static Task<ITestData<ISyntacticVectorComponentNames>> Expression_Empty => Lazy_Expression_Empty.Value;
    public static Task<ITestData<ISyntacticVectorComponentNames>> Expression_String => Lazy_Expression_String.Value;

    private static async Task<ITestData<ISyntacticVectorComponentNames>> CreateExpectedResult_Constructor_StringCollection(IReadOnlyList<string?>? names)
    {
        var quotedNames = StringRepresentationFactory.Create("string", names?.Select(quoteName));

        var source = $$"""
            [SharpMeasures.VectorComponentNames({{quotedNames}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var (namesCollectionLocation, namesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 0);

        VectorComponentNamesSyntax syntax = new(attributeNameLocation, attributeLocation) { NamesCollection = namesCollectionLocation, NamesElements = namesElementLocations };

        SyntacticVectorComponentNames expectedResult = new(syntax) { Names = names };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string quoteName(string? name) => name switch
        {
            null => "null",
            not null => $"\"{name}\""
        };
    }

    private static async Task<ITestData<ISyntacticVectorComponentNames>> CreateExpectedResult_Constructor_String(string? expression)
    {
        var source = $$"""
            [SharpMeasures.VectorComponentNames({{StringRepresentationFactory.Create(expression)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        VectorComponentNamesSyntax syntax = new(attributeNameLocation, attributeLocation) { Expression = expressionLocation };

        SyntacticVectorComponentNames expectedResult = new(syntax) { Expression = expression };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticVectorComponentNames>> CreateExpectedResult_Names(IReadOnlyList<string?>? names) => await CreateExpectedResult_Constructor_StringCollection(names);
    private static async Task<ITestData<ISyntacticVectorComponentNames>> CreateExpectedResult_Expression(string? expression) => await CreateExpectedResult_Constructor_String(expression);

    private sealed class SyntacticVectorComponentNames : ISyntacticVectorComponentNames
    {
        public IReadOnlyList<string?>? Names { get; init; }
        public string? Expression { get; init; }

        public IVectorComponentNamesSyntax Syntax { get; }

        public SyntacticVectorComponentNames(IVectorComponentNamesSyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class VectorComponentNamesSyntax : AAttributeSyntax, IVectorComponentNamesSyntax
    {
        public Location NamesCollection { get; init; } = Location.None;
        public IReadOnlyList<Location> NamesElements { get; init; } = Array.Empty<Location>();

        public Location Expression { get; init; } = Location.None;

        public VectorComponentNamesSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
