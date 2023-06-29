namespace SharpMeasures.Generators.Parsing.Attributes.UnitsCases.UnitDerivationCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal static class UnitDerivationTestData
{
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Constructor_String_String_TypeCollection { get; } = new(CreateExpectedResult_Constructor_String_String_TypeCollection_Populated);
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Constructor_String_TypeCollection { get; } = new(CreateExpectedResult_Constructor_String_TypeCollection_Populated);

    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_DerivationID_Null { get; } = new(() => CreateExpectedResult_DerivationID(null));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_DerivationID_Empty { get; } = new(() => CreateExpectedResult_DerivationID(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_DerivationID_String { get; } = new(() => CreateExpectedResult_DerivationID("A"));

    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Expression_Null { get; } = new(() => CreateExpectedResult_Expression(null));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Expression_Empty { get; } = new(() => CreateExpectedResult_Expression(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Expression_String { get; } = new(() => CreateExpectedResult_Expression("A"));

    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Signature_Null { get; } = new(CreateExpectedResult_Signature_Null);
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Signature_Empty { get; } = new(CreateExpectedResult_Signature_Empty);
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_Signature_Populated { get; } = new(CreateExpectedResult_Signature_Populated);

    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_MethodName_Null { get; } = new(() => CreateExpectedResult_MethodName(null));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_MethodName_Empty { get; } = new(() => CreateExpectedResult_MethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticUnitDerivation>>> Lazy_MethodName_String { get; } = new(() => CreateExpectedResult_MethodName("A"));

    public static Task<ITestData<ISyntacticUnitDerivation>> Constructor_String_String_TypeCollection => Lazy_Constructor_String_String_TypeCollection.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> Constructor_String_TypeCollection => Lazy_Constructor_String_TypeCollection.Value;

    public static Task<ITestData<ISyntacticUnitDerivation>> DerivationID_Null => Lazy_DerivationID_Null.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> DerivationID_Empty => Lazy_DerivationID_Empty.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> DerivationID_String => Lazy_DerivationID_String.Value;

    public static Task<ITestData<ISyntacticUnitDerivation>> Expression_Null => Lazy_Expression_Null.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> Expression_Empty => Lazy_Expression_Empty.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> Expression_String => Lazy_Expression_String.Value;

    public static Task<ITestData<ISyntacticUnitDerivation>> Signature_Null => Lazy_Signature_Null.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> Signature_Empty => Lazy_Signature_Empty.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> Signature_Populated => Lazy_Signature_Populated.Value;

    public static Task<ITestData<ISyntacticUnitDerivation>> MethodName_Null => Lazy_MethodName_Null.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> MethodName_Empty => Lazy_MethodName_Empty.Value;
    public static Task<ITestData<ISyntacticUnitDerivation>> MethodName_String => Lazy_MethodName_String.Value;

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Constructor_String_String_TypeCollection_Populated()
    {
        return await CreateExpectedResult_Constructor_String_String_TypeCollection("A", "B", new[] { "string", "int", null }, signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Constructor_String_String_TypeCollection(string? derivationID, string? expression, IReadOnlyList<string?>? signature, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        var typeofSignature = StringRepresentationFactory.Create("System.Type", signature?.Select(typeofSignatureElement).ToList());

        var source = $$"""
            [SharpMeasures.UnitDerivation({{StringRepresentationFactory.Create(derivationID)}}, {{StringRepresentationFactory.Create(expression)}}, {{typeofSignature}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var derivationIDLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);
        var (signatureCollectionLocation, signatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 2);

        UnitDerivationSyntax syntax = new(attributeNameLocation, attributeLocation, expressionLocation, signatureCollectionLocation, signatureElementLocations) { DerivationID = derivationIDLocation };

        SyntacticUnitDerivation expectedResult = new(expression, signatureSymbols(compilation), syntax) { DerivationID = derivationID };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string typeofSignatureElement(string? signatureElement) => signatureElement switch
        {
            null => "null",
            not null => $"typeof({signatureElement})"
        };
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Constructor_String_TypeCollection_Populated()
    {
        return await CreateExpectedResult_Constructor_String_TypeCollection("A", new[] { "string", "int", null }, signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Constructor_String_TypeCollection(string? expression, IReadOnlyList<string?>? signature, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        var typeofSignature = StringRepresentationFactory.Create("System.Type", signature?.Select(typeofSignatureElement).ToList());

        var source = $$"""
            [SharpMeasures.UnitDerivation({{StringRepresentationFactory.Create(expression)}}, {{typeofSignature}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (signatureCollectionLocation, signatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);

        UnitDerivationSyntax syntax = new(attributeNameLocation, attributeLocation, expressionLocation, signatureCollectionLocation, signatureElementLocations);

        SyntacticUnitDerivation expectedResult = new(expression, signatureSymbols(compilation), syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);

        static string typeofSignatureElement(string? signatureElement) => signatureElement switch
        {
            null => "null",
            not null => $"typeof({signatureElement})"
        };
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_DerivationID(string? derivationID)
    {
        return await CreateExpectedResult_Constructor_String_String_TypeCollection(derivationID, "A", Array.Empty<string?>(), signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => Array.Empty<ITypeSymbol?>();
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Expression(string? expression)
    {
        return await CreateExpectedResult_Constructor_String_String_TypeCollection("A", expression, Array.Empty<string?>(), signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => Array.Empty<ITypeSymbol?>();
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Signature_Empty() => await CreateExpectedResult_Signature(Array.Empty<string?>(), static (compilation) => Array.Empty<ITypeSymbol?>());
    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Signature_Null() => await CreateExpectedResult_Signature(null, static (compilation) => null);
    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Signature_Populated()
    {
        return await CreateExpectedResult_Signature(new[] { "string", "int", null }, signatureSymbols);

        static IReadOnlyList<ITypeSymbol?>? signatureSymbols(Compilation compilation) => new[]
        {
            compilation.GetSpecialType(SpecialType.System_String),
            compilation.GetSpecialType(SpecialType.System_Int32),
            null
        };
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_Signature(IReadOnlyList<string?>? signature, Func<Compilation, IReadOnlyList<ITypeSymbol?>?> signatureSymbols)
    {
        return await CreateExpectedResult_Constructor_String_String_TypeCollection("A", "B", signature, signatureSymbols);
    }

    private static async Task<ITestData<ISyntacticUnitDerivation>> CreateExpectedResult_MethodName(string? methodName)
    {
        var source = $$"""
            [SharpMeasures.UnitDerivation("A", new System.Type[0], MethodName = {{StringRepresentationFactory.Create(methodName)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var expressionLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var (signatureCollectionLocation, signatureElementLocations) = ExpectedLocation.ArrayArgument(attributeSyntax, 1);
        var methodNameLocation = ExpectedLocation.SingleArgument(attributeSyntax, 2);

        UnitDerivationSyntax syntax = new(attributeNameLocation, attributeLocation, expressionLocation, signatureCollectionLocation, signatureElementLocations) { MethodName = methodNameLocation };

        SyntacticUnitDerivation expectedResult = new("A", Array.Empty<ITypeSymbol?>(), syntax) { MethodName = methodName };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticUnitDerivation : ISyntacticUnitDerivation
    {
        public string? DerivationID { get; init; }
        public string? Expression { get; }
        public IReadOnlyList<ITypeSymbol?>? Signature { get; }

        public string? MethodName { get; init; }

        public IUnitDerivationSyntax Syntax { get; }

        public SyntacticUnitDerivation(string? expression, IReadOnlyList<ITypeSymbol?>? signature, IUnitDerivationSyntax syntax)
        {
            Expression = expression;
            Signature = signature;

            Syntax = syntax;
        }
    }

    private sealed class UnitDerivationSyntax : AAttributeSyntax, IUnitDerivationSyntax
    {
        public Location DerivationID { get; init; } = Location.None;
        public Location Expression { get; }

        public Location SignatureCollection { get; }
        public IReadOnlyList<Location> SignatureElements { get; }

        public Location MethodName { get; init; } = Location.None;

        public UnitDerivationSyntax(Location attributeName, Location attribute, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements) : base(attributeName, attribute)
        {
            Expression = expression;

            SignatureCollection = signatureCollection;
            SignatureElements = signatureElements;
        }
    }
}
