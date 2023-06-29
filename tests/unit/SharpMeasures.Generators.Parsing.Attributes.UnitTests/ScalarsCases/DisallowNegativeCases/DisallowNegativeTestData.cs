namespace SharpMeasures.Generators.Parsing.Attributes.ScalarsCases.DisallowNegativeCases;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Parsing.Attributes.Scalars;

using System;
using System.Threading.Tasks;

internal static class DisallowNegativeTestData
{
    private static Lazy<Task<ITestData<ISyntacticDisallowNegative>>> Lazy_Constructor_Empty { get; } = new(CreateExpectedResult_Constructor_Empty);
    private static Lazy<Task<ITestData<ISyntacticDisallowNegative>>> Lazy_Constructor_DisallowNegativeBehaviour { get; } = new(() => CreateExpectedResult_Constructor_DisallowNegativeBehaviour(DisallowNegativeBehaviour.Absolute));

    private static Lazy<Task<ITestData<ISyntacticDisallowNegative>>> Lazy_Behaviour_Unrecognized { get; } = new(() => CreateExpectedResult_Behaviour((DisallowNegativeBehaviour)(-1)));
    private static Lazy<Task<ITestData<ISyntacticDisallowNegative>>> Lazy_Behaviour_Recognized { get; } = new(() => CreateExpectedResult_Behaviour(DisallowNegativeBehaviour.Absolute));

    public static Task<ITestData<ISyntacticDisallowNegative>> Constructor_Empty => Lazy_Constructor_Empty.Value;
    public static Task<ITestData<ISyntacticDisallowNegative>> Constructor_DisallowNegativeBehaviour => Lazy_Constructor_DisallowNegativeBehaviour.Value;

    public static Task<ITestData<ISyntacticDisallowNegative>> Behaviour_Unrecognized => Lazy_Behaviour_Unrecognized.Value;
    public static Task<ITestData<ISyntacticDisallowNegative>> Behaviour_Recognized => Lazy_Behaviour_Recognized.Value;

    private static async Task<ITestData<ISyntacticDisallowNegative>> CreateExpectedResult_Constructor_Empty()
    {
        var source = """
            [SharpMeasures.DisallowNegative]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();

        SyntacticDisallowNegative expectedResult = new(new DisallowNegativeSyntax(attributeNameLocation, attributeLocation));

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticDisallowNegative>> CreateExpectedResult_Constructor_DisallowNegativeBehaviour(DisallowNegativeBehaviour behaviour)
    {
        var source = $$"""
            [SharpMeasures.DisallowNegative({{StringRepresentationFactory.CreateEnum(behaviour)}})]
            public class Foo { }
            """;

        var (_, attributeData, attributeSyntax) = await CompilationStore.GetComponents(source, "Foo");

        var attributeNameLocation = attributeSyntax.Name.GetLocation();
        var attributeLocation = attributeSyntax.GetLocation();
        var behaviourLocation = ExpectedLocation.SingleArgument(attributeSyntax, 0);

        DisallowNegativeSyntax syntax = new(attributeNameLocation, attributeLocation) { Behaviour = behaviourLocation };

        SyntacticDisallowNegative expectedResult = new(syntax) { Behaviour = behaviour };

        return TestData.Create(attributeData, attributeSyntax, expectedResult);
    }

    private static async Task<ITestData<ISyntacticDisallowNegative>> CreateExpectedResult_Behaviour(DisallowNegativeBehaviour behaviour) => await CreateExpectedResult_Constructor_DisallowNegativeBehaviour(behaviour);

    private sealed class SyntacticDisallowNegative : ISyntacticDisallowNegative
    {
        public DisallowNegativeBehaviour? Behaviour { get; init; }

        public IDisallowNegativeSyntax Syntax { get; }

        public SyntacticDisallowNegative(IDisallowNegativeSyntax syntax)
        {
            Syntax = syntax;
        }
    }

    private sealed class DisallowNegativeSyntax : AAttributeSyntax, IDisallowNegativeSyntax
    {
        public Location Behaviour { get; init; } = Location.None;

        public DisallowNegativeSyntax(Location attributeName, Location attribute) : base(attributeName, attribute) { }
    }
}
