namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;
using SharpAttributeParser.Patterns;

/// <summary>Maps the parameters of <see cref="UnitInstanceAttribute"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class UnitInstanceMapper : AAdaptiveMapper<IUnitInstanceRecordBuilder, ISemanticUnitInstanceRecordBuilder>
{
    /// <summary>Instantiates a <see cref="UnitInstanceMapper"/>, mapping the parameters of <see cref="UnitInstanceAttribute"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public UnitInstanceMapper(IAdaptiveMapperDependencyProvider<IUnitInstanceRecordBuilder, ISemanticUnitInstanceRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IUnitInstanceRecordBuilder, ISemanticUnitInstanceRecordBuilder> repository)
    {
        repository.NamedParameters.AddNamedMapping(nameof(UnitInstanceAttribute.Name), (factory) => factory.Create(NullableStringPattern, RecordName, RecordName));
        repository.NamedParameters.AddNamedMapping(nameof(UnitInstanceAttribute.PluralForm), (factory) => factory.Create(NullableStringPattern, RecordPluralForm, RecordPluralForm));
    }

    private static IArgumentPattern<string?> NullableStringPattern(IArgumentPatternFactory factory) => factory.NullableString();

    private static void RecordName(IUnitInstanceRecordBuilder recordBuilder, string? name, ExpressionSyntax syntax) => recordBuilder.WithName(name, syntax);
    private static void RecordName(ISemanticUnitInstanceRecordBuilder recordBuilder, string? name) => recordBuilder.WithName(name);

    private static void RecordPluralForm(IUnitInstanceRecordBuilder recordBuilder, string? pluralForm, ExpressionSyntax syntax) => recordBuilder.WithPluralForm(pluralForm, syntax);
    private static void RecordPluralForm(ISemanticUnitInstanceRecordBuilder recordBuilder, string? pluralForm) => recordBuilder.WithPluralForm(pluralForm);
}
