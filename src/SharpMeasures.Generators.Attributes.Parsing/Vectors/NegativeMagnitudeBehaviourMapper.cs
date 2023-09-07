namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="NegativeMagnitudeBehaviourAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class NegativeMagnitudeBehaviourMapper : AAdaptiveMapper<INegativeMagnitudeBehaviourRecordBuilder, ISemanticNegativeMagnitudeBehaviourRecordBuilder>
{
    /// <summary>Instantiates a <see cref="NegativeMagnitudeBehaviourMapper"/>, mapping the parameters of <see cref="NegativeMagnitudeBehaviourAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public NegativeMagnitudeBehaviourMapper(IAdaptiveMapperDependencyProvider<INegativeMagnitudeBehaviourRecordBuilder, ISemanticNegativeMagnitudeBehaviourRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<INegativeMagnitudeBehaviourRecordBuilder, ISemanticNegativeMagnitudeBehaviourRecordBuilder> repository)
    {
        repository.ConstructorParameters.AddNamedMapping(nameof(NegativeMagnitudeBehaviourAttribute.Behaviour), (factory) => factory.Normal.Create(DisallowNegativeBehaviourPattern, RecordBehaviour, RecordBehaviour));
    }

    private static IArgumentPattern<DisallowNegativeBehaviour> DisallowNegativeBehaviourPattern(IArgumentPatternFactory factory) => factory.Enum<DisallowNegativeBehaviour>();

    private static void RecordBehaviour(INegativeMagnitudeBehaviourRecordBuilder recordBuilder, DisallowNegativeBehaviour unitInstance, ExpressionSyntax syntax) => recordBuilder.WithBehaviour(unitInstance, syntax);
    private static void RecordBehaviour(ISemanticNegativeMagnitudeBehaviourRecordBuilder recordBuilder, DisallowNegativeBehaviour unitInstance) => recordBuilder.WithBehaviour(unitInstance);
}
