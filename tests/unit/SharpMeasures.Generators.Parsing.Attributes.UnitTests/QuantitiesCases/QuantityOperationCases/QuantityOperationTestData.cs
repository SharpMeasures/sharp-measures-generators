namespace SharpMeasures.Generators.Parsing.Attributes.QuantitiesCases.QuantityOperationCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Quantities;

using System;
using System.Threading.Tasks;

internal static class QuantityOperationTestData
{
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_Constructor_Type_Type_OperatorType { get; } = new(CreateExpectedResult_Constructor_Type_Type_OperatorType_Populated);

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_OperatorType_Unrecognized { get; } = new(() => CreateExpectedResult_OperatorType((OperatorType)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_OperatorType_Recognized { get; } = new(() => CreateExpectedResult_OperatorType(OperatorType.Cross));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_Position_Unrecognized { get; } = new(() => CreateExpectedResult_Position((OperationPosition)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_Position_Recognized { get; } = new(() => CreateExpectedResult_Position(OperationPosition.Right));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirrorMode_Unrecognized { get; } = new(() => CreateExpectedResult_MirrorMode((OperationMirrorMode)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirrorMode_Recognized { get; } = new(() => CreateExpectedResult_MirrorMode(OperationMirrorMode.Disabled));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_Implementation_Unrecognized { get; } = new(() => CreateExpectedResult_Implementation((OperationImplementation)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_Implementation_Recognized { get; } = new(() => CreateExpectedResult_Implementation(OperationImplementation.InstanceMethod));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredImplementation_Unrecognized { get; } = new(() => CreateExpectedResult_MirroredImplementation((OperationImplementation)(-1)));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredImplementation_Recognized { get; } = new(() => CreateExpectedResult_MirroredImplementation(OperationImplementation.InstanceMethod));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MethodName_Null { get; } = new(() => CreateExpectedResult_MethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MethodName_Empty { get; } = new(() => CreateExpectedResult_MethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MethodName_String { get; } = new(() => CreateExpectedResult_MethodName("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_StaticMethodName_Null { get; } = new(() => CreateExpectedResult_StaticMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_StaticMethodName_Empty { get; } = new(() => CreateExpectedResult_StaticMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_StaticMethodName_String { get; } = new(() => CreateExpectedResult_StaticMethodName("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredMethodName_Null { get; } = new(() => CreateExpectedResult_MirroredMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredMethodName_Empty { get; } = new(() => CreateExpectedResult_MirroredMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredMethodName_String { get; } = new(() => CreateExpectedResult_MirroredMethodName("A"));

    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredStaticMethodName_Null { get; } = new(() => CreateExpectedResult_MirroredStaticMethodName(null));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredStaticMethodName_Empty { get; } = new(() => CreateExpectedResult_MirroredStaticMethodName(string.Empty));
    private static Lazy<Task<ITestData<ISyntacticQuantityOperation>>> Lazy_MirroredStaticMethodName_String { get; } = new(() => CreateExpectedResult_MirroredStaticMethodName("A"));

    public static Task<ITestData<ISyntacticQuantityOperation>> Constructor_Type_Type_OperatorType => Lazy_Constructor_Type_Type_OperatorType.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> OperatorType_Unrecognized => Lazy_OperatorType_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> OperatorType_Recognized => Lazy_OperatorType_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> Position_Unrecognized => Lazy_Position_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> Position_Recognized => Lazy_Position_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> MirrorMode_Unrecognized => Lazy_MirrorMode_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirrorMode_Recognized => Lazy_MirrorMode_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> Implementation_Unrecognized => Lazy_Implementation_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> Implementation_Recognized => Lazy_Implementation_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredImplementation_Unrecognized => Lazy_MirroredImplementation_Unrecognized.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredImplementation_Recognized => Lazy_MirroredImplementation_Recognized.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> MethodName_Null => Lazy_MethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MethodName_Empty => Lazy_MethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MethodName_String => Lazy_MethodName_String.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> StaticMethodName_Null => Lazy_StaticMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> StaticMethodName_Empty => Lazy_StaticMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> StaticMethodName_String => Lazy_StaticMethodName_String.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredMethodName_Null => Lazy_MirroredMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredMethodName_Empty => Lazy_MirroredMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredMethodName_String => Lazy_MirroredMethodName_String.Value;

    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredStaticMethodName_Null => Lazy_MirroredStaticMethodName_Null.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredStaticMethodName_Empty => Lazy_MirroredStaticMethodName_Empty.Value;
    public static Task<ITestData<ISyntacticQuantityOperation>> MirroredStaticMethodName_String => Lazy_MirroredStaticMethodName_String.Value;

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_Constructor(string result, string other, OperatorType operatorType, Func<Compilation, ITypeSymbol> resultSymbol, Func<Compilation, ITypeSymbol> otherSymbol)
    {
        var source = $$"""
            [SharpMeasures.QuantityOperation<{{result}}, {{other}}>({{StringRepresentationFactory.CreateEnum(operatorType)}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var otherLocation = ExpectedLocation.TypeArgument(attributeSyntax, 1);
        var operatorTypeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        QuantityOperationSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, otherLocation, operatorTypeLocation);

        SyntacticQuantityOperation expectedResult = new(resultSymbol(compilation), otherSymbol(compilation), operatorType, syntax);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_Constructor_Type_Type_OperatorType_Populated()
    {
        return await CreateExpectedResult_Constructor("string", "int", OperatorType.Cross, resultSymbol, static (compilation) => compilation.GetSpecialType(SpecialType.System_Int32));

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_String);
    }

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_OperatorType(OperatorType operatorType)
    {
        return await CreateExpectedResult_Constructor("string", "int", operatorType, resultSymbol, static (compilation) => compilation.GetSpecialType(SpecialType.System_Int32));

        static ITypeSymbol resultSymbol(Compilation compilation) => compilation.GetSpecialType(SpecialType.System_String);
    }

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_Position(OperationPosition position) => await CreateExpectedResult_SingleProperty
    (
        "Position",
        StringRepresentationFactory.CreateEnum(position),
        (quantityOperation) => quantityOperation.Position = position,
        (quantityOperation, location) => quantityOperation.Position = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_MirrorMode(OperationMirrorMode mirrorMode) => await CreateExpectedResult_SingleProperty
    (
        "MirrorMode",
        StringRepresentationFactory.CreateEnum(mirrorMode),
        (quantityOperation) => quantityOperation.MirrorMode = mirrorMode,
        (quantityOperation, location) => quantityOperation.MirrorMode = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_Implementation(OperationImplementation implementation) => await CreateExpectedResult_SingleProperty
    (
        "Implementation",
        StringRepresentationFactory.CreateEnum(implementation),
        (quantityOperation) => quantityOperation.Implementation = implementation,
        (quantityOperation, location) => quantityOperation.Implementation = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_MirroredImplementation(OperationImplementation mirroredImplementation) => await CreateExpectedResult_SingleProperty
    (
        "MirroredImplementation",
        StringRepresentationFactory.CreateEnum(mirroredImplementation),
        (quantityOperation) => quantityOperation.MirroredImplementation = mirroredImplementation,
        (quantityOperation, location) => quantityOperation.MirroredImplementation = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_MethodName(string? methodName) => await CreateExpectedResult_SingleProperty
    (
        "MethodName",
        StringRepresentationFactory.Create(methodName),
        (quantityOperation) => quantityOperation.MethodName = methodName,
        (quantityOperation, location) => quantityOperation.MethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_StaticMethodName(string? staticMethodName) => await CreateExpectedResult_SingleProperty
    (
        "StaticMethodName",
        StringRepresentationFactory.Create(staticMethodName),
        (quantityOperation) => quantityOperation.StaticMethodName = staticMethodName,
        (quantityOperation, location) => quantityOperation.StaticMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_MirroredMethodName(string? mirroredMethodName) => await CreateExpectedResult_SingleProperty
    (
        "MirroredMethodName",
        StringRepresentationFactory.Create(mirroredMethodName),
        (quantityOperation) => quantityOperation.MirroredMethodName = mirroredMethodName,
        (quantityOperation, location) => quantityOperation.MirroredMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_MirroredStaticMethodName(string? mirroredStaticMethodName) => await CreateExpectedResult_SingleProperty
    (
        "MirroredStaticMethodName",
        StringRepresentationFactory.Create(mirroredStaticMethodName),
        (quantityOperation) => quantityOperation.MirroredStaticMethodName = mirroredStaticMethodName,
        (quantityOperation, location) => quantityOperation.MirroredStaticMethodName = location
    );

    private static async Task<ITestData<ISyntacticQuantityOperation>> CreateExpectedResult_SingleProperty(string propertyName, string stringRepresentation, Action<SyntacticQuantityOperation> setter, Action<QuantityOperationSyntax, Location> syntaxSetter)
    {
        var source = $$"""
            [SharpMeasures.QuantityOperation<string, int>({{StringRepresentationFactory.CreateEnum(OperatorType.Cross)}}, {{propertyName}} = {{stringRepresentation}})]
            public class Foo { }
            """;

        var (compilation, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var resultLocation = ExpectedLocation.TypeArgument(attributeSyntax, 0);
        var otherLocation = ExpectedLocation.TypeArgument(attributeSyntax, 1);
        var operatorTypeLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);
        var otherPropertyLocation = ExpectedLocation.SingleArgument(attributeSyntax, 1);

        QuantityOperationSyntax syntax = new(attributeNameLocation, attributeLocation, resultLocation, otherLocation, operatorTypeLocation);

        SyntacticQuantityOperation expectedResult = new(compilation.GetSpecialType(SpecialType.System_String), compilation.GetSpecialType(SpecialType.System_Int32), OperatorType.Cross, syntax);

        syntaxSetter(syntax, otherPropertyLocation);
        setter(expectedResult);

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private sealed class SyntacticQuantityOperation : ISyntacticQuantityOperation
    {
        public ITypeSymbol Result { get; }
        public ITypeSymbol Other { get; }

        public OperatorType OperatorType { get; }
        public OperationPosition? Position { get; set; }

        public OperationMirrorMode? MirrorMode { get; set; }
        public OperationImplementation? Implementation { get; set; }
        public OperationImplementation? MirroredImplementation { get; set; }

        public string? MethodName { get; set; }
        public string? StaticMethodName { get; set; }
        public string? MirroredMethodName { get; set; }
        public string? MirroredStaticMethodName { get; set; }

        public IQuantityOperationSyntax Syntax { get; }

        public SyntacticQuantityOperation(ITypeSymbol result, ITypeSymbol other, OperatorType operatorType, IQuantityOperationSyntax syntax)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;

            Syntax = syntax;
        }
    }

    private sealed class QuantityOperationSyntax : AAttributeSyntax, IQuantityOperationSyntax
    {
        public Location Result { get; }
        public Location Other { get; }

        public Location OperatorType { get; }
        public Location Position { get; set; } = Location.None;
        public Location MirrorMode { get; set; } = Location.None;
        public Location Implementation { get; set; } = Location.None;
        public Location MirroredImplementation { get; set; } = Location.None;

        public Location MethodName { get; set; } = Location.None;
        public Location StaticMethodName { get; set; } = Location.None;
        public Location MirroredMethodName { get; set; } = Location.None;
        public Location MirroredStaticMethodName { get; set; } = Location.None;

        public QuantityOperationSyntax(Location attributeName, Location attribute, Location result, Location other, Location operatorType) : base(attributeName, attribute)
        {
            Result = result;
            Other = other;

            OperatorType = operatorType;
        }
    }
}
