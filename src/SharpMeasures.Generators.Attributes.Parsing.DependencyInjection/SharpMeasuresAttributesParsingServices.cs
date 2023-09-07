namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Documentation;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System;

/// <summary>Allows the services of <i>SharpMeasures.Generators.Attributes.Parsing</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpMeasuresAttributesParsingServices
{
    /// <summary>Registers the services of <i>SharpMeasures.Generators.Attributes.Parsing</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddSharpMeasuresAttributesParsing(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        AddDocumentation(services);
        AddQuantities(services);
        AddScalars(services);
        AddUnits(services);
        AddVectors(services);

        return services;
    }

    private static void AddDocumentation(IServiceCollection services)
    {
        services.AddSingleton<IDisableDocumentationRecordFactory, DisableDocumentationRecordFactory>();
        services.AddSingleton<IEnableDocumentationRecordFactory, EnableDocumentationRecordFactory>();
    }

    private static void AddQuantities(IServiceCollection services)
    {
        services.AddSingleton<IDisableQuantityDifferenceRecordFactory, DisableQuantityDifferenceRecordFactory>();
        services.AddSingleton<IDisableQuantitySumRecordFactory, DisableQuantitySumRecordFactory>();

        services.AddSingleton<ICombinedMapper<IDefaultUnitInstanceRecordBuilder>, DefaultUnitInstanceMapper>();
        services.AddSingleton<ICombinedMapper<IQuantityConversionRecordBuilder>, QuantityConversionMapper>();
        services.AddSingleton<ICombinedMapper<IQuantityDifferenceRecordBuilder>, QuantityDifferenceMapper>();
        services.AddSingleton<ICombinedMapper<IQuantityOperationRecordBuilder>, QuantityOperationMapper>();
        services.AddSingleton<ICombinedMapper<IQuantitySumRecordBuilder>, QuantitySumMapper>();

        services.AddSingleton<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>, DefaultUnitInstanceMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantityConversionRecordBuilder>, QuantityConversionMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantityDifferenceRecordBuilder>, QuantityDifferenceMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantityOperationRecordBuilder>, QuantityOperationMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantitySumRecordBuilder>, QuantitySumMapper>();

        services.AddSingleton<IDefaultUnitInstanceRecorderFactory, DefaultUnitInstanceRecorderFactory>();
        services.AddSingleton<IQuantityConversionRecorderFactory, QuantityConversionRecorderFactory>();
        services.AddSingleton<IQuantityDifferenceRecorderFactory, QuantityDifferenceRecorderFactory>();
        services.AddSingleton<IQuantityOperationRecorderFactory, QuantityOperationRecorderFactory>();
        services.AddSingleton<IQuantitySumRecorderFactory, QuantitySumRecorderFactory>();

        services.AddSingleton<ISemanticDefaultUnitInstanceRecorderFactory, SemanticDefaultUnitInstanceRecorderFactory>();
        services.AddSingleton<ISemanticQuantityConversionRecorderFactory, SemanticQuantityConversionRecorderFactory>();
        services.AddSingleton<ISemanticQuantityDifferenceRecorderFactory, SemanticQuantityDifferenceRecorderFactory>();
        services.AddSingleton<ISemanticQuantityOperationRecorderFactory, SemanticQuantityOperationRecorderFactory>();
        services.AddSingleton<ISemanticQuantitySumRecorderFactory, SemanticQuantitySumRecorderFactory>();

        services.AddSingleton<IDefaultUnitInstanceParser, DefaultUnitInstanceParser>();
        services.AddSingleton<IQuantityConversionParser, QuantityConversionParser>();
        services.AddSingleton<IQuantityDifferenceParser, QuantityDifferenceParser>();
        services.AddSingleton<IQuantityOperationParser, QuantityOperationParser>();
        services.AddSingleton<IQuantitySumParser, QuantitySumParser>();

        services.AddSingleton<ISemanticDefaultUnitInstanceParser, SemanticDefaultUnitInstanceParser>();
        services.AddSingleton<ISemanticQuantityConversionParser, SemanticQuantityConversionParser>();
        services.AddSingleton<ISemanticQuantityDifferenceParser, SemanticQuantityDifferenceParser>();
        services.AddSingleton<ISemanticQuantityOperationParser, SemanticQuantityOperationParser>();
        services.AddSingleton<ISemanticQuantitySumParser, SemanticQuantitySumParser>();
    }

    private static void AddScalars(IServiceCollection services)
    {
        services.AddSingleton<IAllowNegativeRecordFactory, AllowNegativeRecordFactory>();
        services.AddSingleton<IUnitlessQuantityRecordFactory, UnitlessQuantityRecordFactory>();

        services.AddSingleton<ICombinedMapper<IDisallowNegativeRecordBuilder>, DisallowNegativeMapper>();
        services.AddSingleton<ICombinedMapper<IScalarQuantityRecordBuilder>, ScalarQuantityMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedScalarQuantityRecordBuilder>, SpecializedScalarQuantityMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedUnitlessQuantityRecordBuilder>, SpecializedUnitlessQuantityMapper>();
        services.AddSingleton<ICombinedMapper<IVectorAssociationRecordBuilder>, VectorAssociationMapper>();

        services.AddSingleton<ISemanticMapper<ISemanticDisallowNegativeRecordBuilder>, DisallowNegativeMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticScalarQuantityRecordBuilder>, ScalarQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedScalarQuantityRecordBuilder>, SpecializedScalarQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedUnitlessQuantityRecordBuilder>, SpecializedUnitlessQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorAssociationRecordBuilder>, VectorAssociationMapper>();

        services.AddSingleton<IDisallowNegativeRecorderFactory, DisallowNegativeRecorderFactory>();
        services.AddSingleton<IScalarQuantityRecorderFactory, ScalarQuantityRecorderFactory>();
        services.AddSingleton<ISpecializedScalarQuantityRecorderFactory, SpecializedScalarQuantityRecorderFactory>();
        services.AddSingleton<ISpecializedUnitlessQuantityRecorderFactory, SpecializedUnitlessQuantityRecorderFactory>();
        services.AddSingleton<IVectorAssociationRecorderFactory, VectorAssociationRecorderFactory>();

        services.AddSingleton<ISemanticDisallowNegativeRecorderFactory, SemanticDisallowNegativeRecorderFactory>();
        services.AddSingleton<ISemanticScalarQuantityRecorderFactory, SemanticScalarQuantityRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedScalarQuantityRecorderFactory, SemanticSpecializedScalarQuantityRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedUnitlessQuantityRecorderFactory, SemanticSpecializedUnitlessQuantityRecorderFactory>();
        services.AddSingleton<ISemanticVectorAssociationRecorderFactory, SemanticVectorAssociationRecorderFactory>();

        services.AddSingleton<IDisallowNegativeParser, DisallowNegativeParser>();
        services.AddSingleton<IScalarQuantityParser, ScalarQuantityParser>();
        services.AddSingleton<ISpecializedScalarQuantityParser, SpecializedScalarQuantityParser>();
        services.AddSingleton<ISpecializedUnitlessQuantityParser, SpecializedUnitlessQuantityParser>();
        services.AddSingleton<IVectorAssociationParser, VectorAssociationParser>();

        services.AddSingleton<ISemanticDisallowNegativeParser, SemanticDisallowNegativeParser>();
        services.AddSingleton<ISemanticScalarQuantityParser, SemanticScalarQuantityParser>();
        services.AddSingleton<ISemanticSpecializedScalarQuantityParser, SemanticSpecializedScalarQuantityParser>();
        services.AddSingleton<ISemanticSpecializedUnitlessQuantityParser, SemanticSpecializedUnitlessQuantityParser>();
        services.AddSingleton<ISemanticVectorAssociationParser, SemanticVectorAssociationParser>();
    }

    private static void AddUnits(IServiceCollection services)
    {
        services.AddSingleton<ICombinedMapper<IUnitRecordBuilder>, UnitMapper>();
        services.AddSingleton<ICombinedMapper<IUnitInstanceRecordBuilder>, UnitInstanceMapper>();

        services.AddSingleton<ISemanticMapper<ISemanticUnitRecordBuilder>, UnitMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticUnitInstanceRecordBuilder>, UnitInstanceMapper>();

        services.AddSingleton<IUnitRecorderFactory, UnitRecorderFactory>();
        services.AddSingleton<IUnitInstanceRecorderFactory, UnitInstanceRecorderFactory>();

        services.AddSingleton<ISemanticUnitRecorderFactory, SemanticUnitRecorderFactory>();
        services.AddSingleton<ISemanticUnitInstanceRecorderFactory, SemanticUnitInstanceRecorderFactory>();

        services.AddSingleton<IUnitParser, UnitParser>();
        services.AddSingleton<IUnitInstanceParser, UnitInstanceParser>();

        services.AddSingleton<ISemanticUnitParser, SemanticUnitParser>();
        services.AddSingleton<ISemanticUnitInstanceParser, SemanticUnitInstanceParser>();
    }

    private static void AddVectors(IServiceCollection services)
    {
        services.AddSingleton<ICombinedMapper<INegativeMagnitudeBehaviourRecordBuilder>, NegativeMagnitudeBehaviourMapper>();
        services.AddSingleton<ICombinedMapper<IScalarAssociationRecordBuilder>, ScalarAssociationMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedVectorGroupRecordBuilder>, SpecializedVectorGroupMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedVectorQuantityRecordBuilder>, SpecializedVectorQuantityMapper>();
        services.AddSingleton<ICombinedMapper<IVectorComponentNamesRecordBuilder>, VectorComponentNamesMapper>();
        services.AddSingleton<ICombinedMapper<IVectorGroupRecordBuilder>, VectorGroupMapper>();
        services.AddSingleton<ICombinedMapper<IVectorGroupMemberRecordBuilder>, VectorGroupMemberMapper>();
        services.AddSingleton<ICombinedMapper<IVectorQuantityRecordBuilder>, VectorQuantityMapper>();

        services.AddSingleton<ISemanticMapper<ISemanticNegativeMagnitudeBehaviourRecordBuilder>, NegativeMagnitudeBehaviourMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticScalarAssociationRecordBuilder>, ScalarAssociationMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedVectorGroupRecordBuilder>, SpecializedVectorGroupMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedVectorQuantityRecordBuilder>, SpecializedVectorQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorComponentNamesRecordBuilder>, VectorComponentNamesMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorGroupRecordBuilder>, VectorGroupMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorGroupMemberRecordBuilder>, VectorGroupMemberMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorQuantityRecordBuilder>, VectorQuantityMapper>();

        services.AddSingleton<INegativeMagnitudeBehaviourRecorderFactory, NegativeMagnitudeBehaviourRecorderFactory>();
        services.AddSingleton<IScalarAssociationRecorderFactory, ScalarAssociationRecorderFactory>();
        services.AddSingleton<ISpecializedVectorGroupRecorderFactory, SpecializedVectorGroupRecorderFactory>();
        services.AddSingleton<ISpecializedVectorQuantityRecorderFactory, SpecializedVectorQuantityRecorderFactory>();
        services.AddSingleton<IVectorComponentNamesRecorderFactory, VectorComponentNamesRecorderFactory>();
        services.AddSingleton<IVectorGroupRecorderFactory, VectorGroupRecorderFactory>();
        services.AddSingleton<IVectorGroupMemberRecorderFactory, VectorGroupMemberRecorderFactory>();
        services.AddSingleton<IVectorQuantityRecorderFactory, VectorQuantityRecorderFactory>();

        services.AddSingleton<ISemanticNegativeMagnitudeBehaviourRecorderFactory, SemanticNegativeMagnitudeBehaviourRecorderFactory>();
        services.AddSingleton<ISemanticScalarAssociationRecorderFactory, SemanticScalarAssociationRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedVectorGroupRecorderFactory, SemanticSpecializedVectorGroupRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedVectorQuantityRecorderFactory, SemanticSpecializedVectorQuantityRecorderFactory>();
        services.AddSingleton<ISemanticVectorComponentNamesRecorderFactory, SemanticVectorComponentNamesRecorderFactory>();
        services.AddSingleton<ISemanticVectorGroupRecorderFactory, SemanticVectorGroupRecorderFactory>();
        services.AddSingleton<ISemanticVectorGroupMemberRecorderFactory, SemanticVectorGroupMemberRecorderFactory>();
        services.AddSingleton<ISemanticVectorQuantityRecorderFactory, SemanticVectorQuantityRecorderFactory>();

        services.AddSingleton<INegativeMagnitudeBehaviourParser, NegativeMagnitudeBehaviourParser>();
        services.AddSingleton<IScalarAssociationParser, ScalarAssociationParser>();
        services.AddSingleton<ISpecializedVectorGroupParser, SpecializedVectorGroupParser>();
        services.AddSingleton<ISpecializedVectorQuantityParser, SpecializedVectorQuantityParser>();
        services.AddSingleton<IVectorComponentNamesParser, VectorComponentNamesParser>();
        services.AddSingleton<IVectorGroupParser, VectorGroupParser>();
        services.AddSingleton<IVectorGroupMemberParser, VectorGroupMemberParser>();
        services.AddSingleton<IVectorQuantityParser, VectorQuantityParser>();

        services.AddSingleton<ISemanticNegativeMagnitudeBehaviourParser, SemanticNegativeMagnitudeBehaviourParser>();
        services.AddSingleton<ISemanticScalarAssociationParser, SemanticScalarAssociationParser>();
        services.AddSingleton<ISemanticSpecializedVectorGroupParser, SemanticSpecializedVectorGroupParser>();
        services.AddSingleton<ISemanticSpecializedVectorQuantityParser, SemanticSpecializedVectorQuantityParser>();
        services.AddSingleton<ISemanticVectorComponentNamesParser, SemanticVectorComponentNamesParser>();
        services.AddSingleton<ISemanticVectorGroupParser, SemanticVectorGroupParser>();
        services.AddSingleton<ISemanticVectorGroupMemberParser, SemanticVectorGroupMemberParser>();
        services.AddSingleton<ISemanticVectorQuantityParser, SemanticVectorQuantityParser>();
    }
}
