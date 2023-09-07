namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.Repositories.Adaptive;

/// <summary>Maps the parameters of <see cref="ExtendedUnitAttribute{TOriginal}"/> to recorders, responsible for recording arguments of that parameter.</summary>
public sealed class ExtendedUnitMapper : AAdaptiveMapper<IExtendedUnitRecordBuilder, ISemanticExtendedUnitRecordBuilder>
{
    /// <summary>Instantiates a <see cref="ExtendedUnitMapper"/>, mapping the parameters of <see cref="ExtendedUnitAttribute{TOriginal}"/> to recorders.</summary>
    /// <param name="dependencyProvider">Provides the dependencies of the mapper.</param>
    public ExtendedUnitMapper(IAdaptiveMapperDependencyProvider<IExtendedUnitRecordBuilder, ISemanticExtendedUnitRecordBuilder> dependencyProvider) : base(dependencyProvider) { }

    /// <inheritdoc/>
    protected override void AddMappings(IAppendableAdaptiveMappingRepository<IExtendedUnitRecordBuilder, ISemanticExtendedUnitRecordBuilder> repository)
    {
        repository.TypeParameters.AddIndexedMapping(0, (factory) => factory.Create(RecordOriginal, RecordOriginal));
    }

    private static void RecordOriginal(IExtendedUnitRecordBuilder recordBuilder, ITypeSymbol original, ExpressionSyntax syntax) => recordBuilder.WithOriginal(original, syntax);
    private static void RecordOriginal(ISemanticExtendedUnitRecordBuilder recordBuilder, ITypeSymbol original) => recordBuilder.WithOriginal(original);
}
