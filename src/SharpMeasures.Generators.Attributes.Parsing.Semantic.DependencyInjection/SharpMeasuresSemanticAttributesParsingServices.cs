namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System;

/// <summary>Allows the services of <i>SharpMeasures.Generators.Attributes.Parsing.Semantic</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpMeasuresSemanticAttributesParsingServices
{
    /// <summary>Registers the services of <i>SharpMeasures.Generators.Attributes.Parsing.Semantic</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresSemanticAttributesParsing(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<ISemanticMapper<ISemanticTypeConversionRecordBuilder>, TypeConversionMapper>();
        services.AddSingleton<ISemanticTypeConversionRecorderFactory, SemanticTypeConversionRecorderFactory>();
        services.AddSingleton<ISemanticTypeConversionParser, SemanticTypeConversionParser>();

        AddQuantities(services);
        AddScalars(services);
        AddUnits(services);
        AddVectors(services);

        return services;
    }

    private static void AddQuantities(IServiceCollection services)
    {
        services.AddSingleton<ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder>, DefaultUnitInstanceMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantityDifferenceRecordBuilder>, QuantityDifferenceMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantityOperationRecordBuilder>, QuantityOperationMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticQuantitySumRecordBuilder>, QuantitySumMapper>();

        services.AddSingleton<ISemanticDefaultUnitInstanceRecorderFactory, SemanticDefaultUnitInstanceRecorderFactory>();
        services.AddSingleton<ISemanticQuantityDifferenceRecorderFactory, SemanticQuantityDifferenceRecorderFactory>();
        services.AddSingleton<ISemanticQuantityOperationRecorderFactory, SemanticQuantityOperationRecorderFactory>();
        services.AddSingleton<ISemanticQuantitySumRecorderFactory, SemanticQuantitySumRecorderFactory>();

        services.AddSingleton<ISemanticDefaultUnitInstanceParser, SemanticDefaultUnitInstanceParser>();
        services.AddSingleton<ISemanticQuantityDifferenceParser, SemanticQuantityDifferenceParser>();
        services.AddSingleton<ISemanticQuantityOperationParser, SemanticQuantityOperationParser>();
        services.AddSingleton<ISemanticQuantitySumParser, SemanticQuantitySumParser>();
    }

    private static void AddScalars(IServiceCollection services)
    {
        services.AddSingleton<ISemanticMapper<ISemanticDisallowNegativeRecordBuilder>, DisallowNegativeMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticScalarQuantityRecordBuilder>, ScalarQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedScalarQuantityRecordBuilder>, SpecializedScalarQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedUnitlessQuantityRecordBuilder>, SpecializedUnitlessQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorAssociationRecordBuilder>, VectorAssociationMapper>();

        services.AddSingleton<ISemanticDisallowNegativeRecorderFactory, SemanticDisallowNegativeRecorderFactory>();
        services.AddSingleton<ISemanticScalarQuantityRecorderFactory, SemanticScalarQuantityRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedScalarQuantityRecorderFactory, SemanticSpecializedScalarQuantityRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedUnitlessQuantityRecorderFactory, SemanticSpecializedUnitlessQuantityRecorderFactory>();
        services.AddSingleton<ISemanticVectorAssociationRecorderFactory, SemanticVectorAssociationRecorderFactory>();

        services.AddSingleton<ISemanticDisallowNegativeParser, SemanticDisallowNegativeParser>();
        services.AddSingleton<ISemanticScalarQuantityParser, SemanticScalarQuantityParser>();
        services.AddSingleton<ISemanticSpecializedScalarQuantityParser, SemanticSpecializedScalarQuantityParser>();
        services.AddSingleton<ISemanticSpecializedUnitlessQuantityParser, SemanticSpecializedUnitlessQuantityParser>();
        services.AddSingleton<ISemanticVectorAssociationParser, SemanticVectorAssociationParser>();
    }

    private static void AddUnits(IServiceCollection services)
    {
        services.AddSingleton<ISemanticMapper<ISemanticUnitRecordBuilder>, UnitMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticUnitInstanceRecordBuilder>, UnitInstanceMapper>();

        services.AddSingleton<ISemanticUnitRecorderFactory, SemanticUnitRecorderFactory>();
        services.AddSingleton<ISemanticUnitInstanceRecorderFactory, SemanticUnitInstanceRecorderFactory>();

        services.AddSingleton<ISemanticUnitParser, SemanticUnitParser>();
        services.AddSingleton<ISemanticUnitInstanceParser, SemanticUnitInstanceParser>();
    }

    private static void AddVectors(IServiceCollection services)
    {
        services.AddSingleton<ISemanticMapper<ISemanticNegativeMagnitudeBehaviourRecordBuilder>, NegativeMagnitudeBehaviourMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticScalarAssociationRecordBuilder>, ScalarAssociationMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedVectorGroupRecordBuilder>, SpecializedVectorGroupMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticSpecializedVectorQuantityRecordBuilder>, SpecializedVectorQuantityMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorComponentNamesRecordBuilder>, VectorComponentNamesMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorGroupRecordBuilder>, VectorGroupMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorGroupMemberRecordBuilder>, VectorGroupMemberMapper>();
        services.AddSingleton<ISemanticMapper<ISemanticVectorQuantityRecordBuilder>, VectorQuantityMapper>();

        services.AddSingleton<ISemanticNegativeMagnitudeBehaviourRecorderFactory, SemanticNegativeMagnitudeBehaviourRecorderFactory>();
        services.AddSingleton<ISemanticScalarAssociationRecorderFactory, SemanticScalarAssociationRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedVectorGroupRecorderFactory, SemanticSpecializedVectorGroupRecorderFactory>();
        services.AddSingleton<ISemanticSpecializedVectorQuantityRecorderFactory, SemanticSpecializedVectorQuantityRecorderFactory>();
        services.AddSingleton<ISemanticVectorComponentNamesRecorderFactory, SemanticVectorComponentNamesRecorderFactory>();
        services.AddSingleton<ISemanticVectorGroupRecorderFactory, SemanticVectorGroupRecorderFactory>();
        services.AddSingleton<ISemanticVectorGroupMemberRecorderFactory, SemanticVectorGroupMemberRecorderFactory>();
        services.AddSingleton<ISemanticVectorQuantityRecorderFactory, SemanticVectorQuantityRecorderFactory>();

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
