namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityProcessCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal static class QuantityProcessTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Constructor_Type_String_String { get; } = new(CreateExpectedResult_Constructor_Type_String_String_Populated);
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Constructor_Type_String_String_TypeCollection { get; } = new(CreateExpectedResult_Constructor_Type_String_String_TypeCollection_Populated);
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Constructor_Type_String_String_TypeCollection_StringCollection { get; } = new(CreateExpectedResult_Constructor_Type_String_String_TypeCollection_StringCollection_Populated);

    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Name_Null { get; } = new(() => CreateExpectedResult_Name(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Name_Empty { get; } = new(() => CreateExpectedResult_Name(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Name_String { get; } = new(() => CreateExpectedResult_Name("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Expression_Null { get; } = new(() => CreateExpectedResult_Expression(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Expression_Empty { get; } = new(() => CreateExpectedResult_Expression(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Expression_String { get; } = new(() => CreateExpectedResult_Expression("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Signature_Null { get; } = new(CreateExpectedResult_Signature_Null);
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Signature_Empty { get; } = new(CreateExpectedResult_Signature_Empty);
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_Signature_Populated { get; } = new(CreateExpectedResult_Signature_Populated);

    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_ParameterNames_Null { get; } = new(() => CreateExpectedResult_ParameterNames(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_ParameterNames_Empty { get; } = new(() => CreateExpectedResult_ParameterNames(Array.Empty<string>()));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_ParameterNames_Populated { get; } = new(() => CreateExpectedResult_ParameterNames(new[] { "A", string.Empty, null }));

    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_ImplementStatically_True { get; } = new(() => CreateExpectedResult_ImplementStatically(true));
    private static Lazy<Task<ITestData<ISyntacticQuantityProcess>>> Lazy_ImplementStatically_False { get; } = new(() => CreateExpectedResult_ImplementStatically(false));

    public static Task<ITestData<ISyntacticQuantityProcess>> Constructor_Type_String_String => Lazy_Constructor_Type_String_String.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Constructor_Type_String_String_TypeCollection => Lazy_Constructor_Type_String_String_TypeCollection.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Constructor_Type_String_String_TypeCollection_StringCollection => Lazy_Constructor_Type_String_String_TypeCollection_StringCollection.Value;

    public static Task<ITestData<ISyntacticQuantityProcess>> Name_Null => Lazy_Name_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Name_Empty => Lazy_Name_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Name_String => Lazy_Name_String.Value;

    public static Task<ITestData<ISyntacticQuantityProcess>> Expression_Null => Lazy_Expression_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Expression_Empty => Lazy_Expression_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Expression_String => Lazy_Expression_String.Value;

    public static Task<ITestData<ISyntacticQuantityProcess>> Signature_Null => Lazy_Signature_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Signature_Empty => Lazy_Signature_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> Signature_Populated => Lazy_Signature_Populated.Value;

    public static Task<ITestData<ISyntacticQuantityProcess>> ParameterNames_Null => Lazy_ParameterNames_Null.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> ParameterNames_Empty => Lazy_ParameterNames_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> ParameterNames_Populated => Lazy_ParameterNames_Populated.Value;

    public static Task<ITestData<ISyntacticQuantityProcess>> ImplementStatically_True => Lazy_ImplementStatically_True.Value;
    public static Task<ITestData<ISyntacticQuantityProcess>> ImplementStatically_False => Lazy_ImplementStatically_False.Value;

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor(string result, string? name, string? expression, Func<Compilation, ITypeSymbol> resultSymbol)
    {
        var source = $$"""
            [SharpMeasures.QuantityProcess<{{result}}>({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(expression)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        QuantityProcessSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, nameLocation, expressionLocation);

        SyntacticQuantityProcess expectedResult = new(resultSymbol(compilation), name, expression, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor(string result, string? name, string? expression, IReadOnlyList<string?>? signature,
        Func<Compilation, ITypeSymbol> resultSymbol, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        var typeofSignature = StringRepresentationFactory.Create("System.Type", signature?.Select(typeofSignatureElement).ToList());

        var source = $$"""
            [SharpMeasures.QuantityProcess<{{result}}>({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(expression)}}, {{typeofSignature}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (signatureCollectionLocation, signatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        QuantityProcessSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, nameLocation, expressionLocation)
        {
            SignatureCollection = signatureCollectionLocation,
            SignatureElements = signatureElementLocations
        };

        SyntacticQuantityProcess expectedResult = new(resultSymbol(compilation), name, expression, syntax) { Signature = signatureSymbols(compilation) };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string typeofSignatureElement(string? signatureElement) => signatureElement switch
        {
            null => "null",
            not null => $"typeof({signatureElement})"
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor(string result, string? name, string? expression, IReadOnlyList<string?>? signature,
        IReadOnlyList<string?>? parameterNames, Func<Compilation, ITypeSymbol> resultSymbol, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        var typeofSignature = StringRepresentationFactory.Create("System.Type", signature?.Select(typeofSignatureElement).ToList());
        var quotedParameterNames = StringRepresentationFactory.Create("string", parameterNames?.Select(quoteParameterName).ToList());

        var source = $$"""
            [SharpMeasures.QuantityProcess<{{result}}>({{StringRepresentationFactory.Create(name)}}, {{StringRepresentationFactory.Create(expression)}}, {{typeofSignature}}, {{quotedParameterNames}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (signatureCollectionLocation, signatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);
        var (parameterNamesCollectionLocation, parameterNamesElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 3);

        QuantityProcessSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, nameLocation, expressionLocation)
        {
            SignatureCollection = signatureCollectionLocation,
            SignatureElements = signatureElementLocations,
            ParameterNamesCollection = parameterNamesCollectionLocation,
            ParameterNamesElements = parameterNamesElementLocations
        };

        SyntacticQuantityProcess expectedResult = new(resultSymbol(compilation), name, expression, syntax) { Signature = signatureSymbols(compilation), ParameterNames = parameterNames };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string typeofSignatureElement(string? signatureElement) => signatureElement switch
        {
            null => "null",
            not null => $"typeof({signatureElement})"
        };

        static string quoteParameterName(string? parameterName) => parameterName switch
        {
            null => "null",
            not null => $"\"{parameterName}\""
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor_Type_String_String_Populated()
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor_Type_String_String_TypeCollection_Populated()
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", new[] { "string", "int", null }, resultSymbol, signatureSymbols);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Constructor_Type_String_String_TypeCollection_StringCollection_Populated()
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", new[] { "string", "int", null }, new[] { "C", string.Empty, null }, resultSymbol, signatureSymbols);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Name(string? name)
    {
        return await CreateExpectedResult_Constructor("int", name, "B", resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Expression(string? expression)
    {
        return await CreateExpectedResult_Constructor("int", "A", expression, resultSymbol);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Signature_Null()
    {
        return await CreateExpectedResult_Signature(null, signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => null;
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Signature_Empty()
    {
        return await CreateExpectedResult_Signature(Array.Empty<string?>(), signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => Array.Empty<ITypeSymbol?>();
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Signature_Populated()
    {
        return await CreateExpectedResult_Signature(new[] { "string", "int", null }, signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_Signature(IReadOnlyList<string?>? signature, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", signature, resultSymbol, signatureSymbols);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_ParameterNames(IReadOnlyList<string?>? parameterNames)
    {
        return await CreateExpectedResult_Constructor("int", "A", "B", new[] { "string", "int", null }, parameterNames, resultSymbol, signatureSymbols);

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_Int32);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticQuantityProcess>> CreateExpectedResult_ImplementStatically(bool implementStatically)
    {
        var source = $$"""
            [SharpMeasures.QuantityProcess<string>("Name", "Expression", ImplementStatically = {{StringRepresentationFactory.Create(implementStatically)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var nameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var implementStaticallyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        QuantityProcessSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, nameLocation, expressionLocation) { ImplementStatically = implementStaticallyLocation };

        SyntacticQuantityProcess expectedResult = new(compilation.GetSpecialType(SpecialType.System_String), "Name", "Expression", syntax) { ImplementStatically = implementStatically };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticQuantityProcess : ISyntacticQuantityProcess
    {
        public ITypeSymbol Result { get; }

        public string? Name { get; }
        public string? Expression { get; }

        public IReadOnlyList<ITypeSymbol?>? Signature { get; init; }
        public IReadOnlyList<string?>? ParameterNames { get; init; }

        public bool? ImplementStatically { get; init; }

        public IQuantityProcessSyntax Syntax { get; init; }

        public SyntacticQuantityProcess(ITypeSymbol result, string? name, string? expression, IQuantityProcessSyntax syntax)
        {
            Result = result;

            Name = name;
            Expression = expression;

            Syntax = syntax;
        }
    }

    private sealed class QuantityProcessSyntax : AAttributeSyntax, IQuantityProcessSyntax
    {
        public Location Result { get; }

        public Location Name { get; }
        public Location Expression { get; }

        public Location SignatureCollection { get; init; } = Location.None;
        public IReadOnlyList<Location> SignatureElements { get; init; } = Array.Empty<Location>();

        public Location ParameterNamesCollection { get; init; } = Location.None;
        public IReadOnlyList<Location> ParameterNamesElements { get; init; } = Array.Empty<Location>();

        public Location ImplementStatically { get; init; } = Location.None;

        public QuantityProcessSyntax(Location attributeName, Location attribute, Location result, Location name, Location expression) : base(attributeName, attribute)
        {
            Result = result;

            Name = name;
            Expression = expression;
        }
    }
}
