namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

using System.Collections.Generic;

/// <summary>Maps the parameters of <see cref="VectorComponentNamesAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class VectorComponentNamesMapper : AAdaptiveMapper<IVectorComponentNamesRecordBuilder, ISemanticVectorComponentNamesRecordBuilder>
{
    /// <summary>Instantiates a <see cref="VectorComponentNamesMapper"/>, mapping the parameters of <see cref="VectorComponentNamesAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public VectorComponentNamesMapper(IAdaptiveMapperDependencyProvider<IVectorComponentNamesRecordBuilder, ISemanticVectorComponentNamesRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IVectorComponentNamesRecordBuilder, ISemanticVectorComponentNamesRecordBuilder> repository)
    {
        repository.NamedParameters.AddNamedMapping(nameof(VectorComponentNamesAttribute.Names), (factory) => factory.Create(NullableStringArrayPattern, RecordNames, RecordNames));
        repository.NamedParameters.AddNamedMapping(nameof(VectorComponentNamesAttribute.Expression), (factory) => factory.Create(NullableStringPattern, RecordExpression, RecordExpression));
    }

    private static IArgumentPattern<string?[]?> NullableStringArrayPattern(IArgumentPatternFactory factory) => factory.NullableArray(factory.NullableString());
    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordNames(IVectorComponentNamesRecordBuilder recordBuilder, IReadOnlyList<string?>? names, ExpressionSyntax syntax) => recordBuilder.WithNames(names, syntax);
    private static void RecordNames(ISemanticVectorComponentNamesRecordBuilder recordBuilder, IReadOnlyList<string?>? names) => recordBuilder.WithNames(names);

    private static void RecordExpression(IVectorComponentNamesRecordBuilder recordBuilder, string? expression, ExpressionSyntax syntax) => recordBuilder.WithExpression(expression, syntax);
    private static void RecordExpression(ISemanticVectorComponentNamesRecordBuilder recordBuilder, string? expression) => recordBuilder.WithExpression(expression);
}
