namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="DisallowNegativeAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class DisallowNegativeMapper : AAdaptiveMapper<IDisallowNegativeRecordBuilder, ISemanticDisallowNegativeRecordBuilder>
{
    /// <summary>Instantiates a <see cref="DisallowNegativeMapper"/>, mapping the parameters of <see cref="DisallowNegativeAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public DisallowNegativeMapper(IAdaptiveMapperDependencyProvider<IDisallowNegativeRecordBuilder, ISemanticDisallowNegativeRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IDisallowNegativeRecordBuilder, ISemanticDisallowNegativeRecordBuilder> repository)
    {
        repository.ConstructorParameters.AddNamedMapping(nameof(DisallowNegativeAttribute.Behaviour), (factory) => factory.Normal.Create(DisallowNegativeBehaviourPattern, RecordBehaviour, RecordBehaviour));
    }

    private static IArgumentPattern<DisallowNegativeBehaviour> DisallowNegativeBehaviourPattern(IArgumentPatternFactory factory) => factory.Enum<DisallowNegativeBehaviour>();

    private static void RecordBehaviour(IDisallowNegativeRecordBuilder recordBuilder, DisallowNegativeBehaviour unitInstance, ExpressionSyntax syntax) => recordBuilder.WithBehaviour(unitInstance, syntax);
    private static void RecordBehaviour(ISemanticDisallowNegativeRecordBuilder recordBuilder, DisallowNegativeBehaviour unitInstance) => recordBuilder.WithBehaviour(unitInstance);
}
