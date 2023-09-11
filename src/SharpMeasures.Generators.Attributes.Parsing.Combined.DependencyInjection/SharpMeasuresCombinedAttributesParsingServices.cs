namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Parsing.Documentation;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;

using System;

/// <summary>Allows the services of <i>SharpMeasures.Generators.Attributes.Parsing.Combined</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpMeasuresCombinedAttributesParsingServices
{
    /// <summary>Registers the services of <i>SharpMeasures.Generators.Attributes.Parsing.Combined</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresCombinedAttributesParsing(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<ICombinedMapper<ITypeConversionRecordBuilder>, TypeConversionMapper>();
        services.AddSingleton<ITypeConversionRecorderFactory, TypeConversionRecorderFactory>();
        services.AddSingleton<ITypeConversionParser, TypeConversionParser>();

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
        services.AddSingleton<ICombinedMapper<IQuantityDifferenceRecordBuilder>, QuantityDifferenceMapper>();
        services.AddSingleton<ICombinedMapper<IQuantityOperationRecordBuilder>, QuantityOperationMapper>();
        services.AddSingleton<ICombinedMapper<IQuantitySumRecordBuilder>, QuantitySumMapper>();

        services.AddSingleton<IDefaultUnitInstanceRecorderFactory, DefaultUnitInstanceRecorderFactory>();
        services.AddSingleton<IQuantityDifferenceRecorderFactory, QuantityDifferenceRecorderFactory>();
        services.AddSingleton<IQuantityOperationRecorderFactory, QuantityOperationRecorderFactory>();
        services.AddSingleton<IQuantitySumRecorderFactory, QuantitySumRecorderFactory>();

        services.AddSingleton<IDefaultUnitInstanceParser, DefaultUnitInstanceParser>();
        services.AddSingleton<IQuantityDifferenceParser, QuantityDifferenceParser>();
        services.AddSingleton<IQuantityOperationParser, QuantityOperationParser>();
        services.AddSingleton<IQuantitySumParser, QuantitySumParser>();
    }

    private static void AddScalars(IServiceCollection services)
    {
        services.AddSingleton<IAllowNegativeRecordFactory, AllowNegativeRecordFactory>();
        services.AddSingleton<IQuantityConstantRecordFactory, QuantityConstantRecordFactory>();
        services.AddSingleton<IUnitlessQuantityRecordFactory, UnitlessQuantityRecordFactory>();

        services.AddSingleton<ICombinedMapper<IDisallowNegativeRecordBuilder>, DisallowNegativeMapper>();
        services.AddSingleton<ICombinedMapper<IScalarQuantityRecordBuilder>, ScalarQuantityMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedScalarQuantityRecordBuilder>, SpecializedScalarQuantityMapper>();
        services.AddSingleton<ICombinedMapper<ISpecializedUnitlessQuantityRecordBuilder>, SpecializedUnitlessQuantityMapper>();
        services.AddSingleton<ICombinedMapper<IVectorAssociationRecordBuilder>, VectorAssociationMapper>();

        services.AddSingleton<IDisallowNegativeRecorderFactory, DisallowNegativeRecorderFactory>();
        services.AddSingleton<IScalarQuantityRecorderFactory, ScalarQuantityRecorderFactory>();
        services.AddSingleton<ISpecializedScalarQuantityRecorderFactory, SpecializedScalarQuantityRecorderFactory>();
        services.AddSingleton<ISpecializedUnitlessQuantityRecorderFactory, SpecializedUnitlessQuantityRecorderFactory>();
        services.AddSingleton<IVectorAssociationRecorderFactory, VectorAssociationRecorderFactory>();

        services.AddSingleton<IDisallowNegativeParser, DisallowNegativeParser>();
        services.AddSingleton<IScalarQuantityParser, ScalarQuantityParser>();
        services.AddSingleton<ISpecializedScalarQuantityParser, SpecializedScalarQuantityParser>();
        services.AddSingleton<ISpecializedUnitlessQuantityParser, SpecializedUnitlessQuantityParser>();
        services.AddSingleton<IVectorAssociationParser, VectorAssociationParser>();
    }

    private static void AddUnits(IServiceCollection services)
    {
        services.AddSingleton<ICombinedMapper<IExtendedUnitRecordBuilder>, ExtendedUnitMapper>();
        services.AddSingleton<ICombinedMapper<IUnitRecordBuilder>, UnitMapper>();
        services.AddSingleton<ICombinedMapper<IUnitInstanceRecordBuilder>, UnitInstanceMapper>();

        services.AddSingleton<IExtendedUnitRecorderFactory, ExtendedUnitRecorderFactory>();
        services.AddSingleton<IUnitRecorderFactory, UnitRecorderFactory>();
        services.AddSingleton<IUnitInstanceRecorderFactory, UnitInstanceRecorderFactory>();

        services.AddSingleton<IExtendedUnitParser, ExtendedUnitParser>();
        services.AddSingleton<IUnitParser, UnitParser>();
        services.AddSingleton<IUnitInstanceParser, UnitInstanceParser>();
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

        services.AddSingleton<INegativeMagnitudeBehaviourRecorderFactory, NegativeMagnitudeBehaviourRecorderFactory>();
        services.AddSingleton<IScalarAssociationRecorderFactory, ScalarAssociationRecorderFactory>();
        services.AddSingleton<ISpecializedVectorGroupRecorderFactory, SpecializedVectorGroupRecorderFactory>();
        services.AddSingleton<ISpecializedVectorQuantityRecorderFactory, SpecializedVectorQuantityRecorderFactory>();
        services.AddSingleton<IVectorComponentNamesRecorderFactory, VectorComponentNamesRecorderFactory>();
        services.AddSingleton<IVectorGroupRecorderFactory, VectorGroupRecorderFactory>();
        services.AddSingleton<IVectorGroupMemberRecorderFactory, VectorGroupMemberRecorderFactory>();
        services.AddSingleton<IVectorQuantityRecorderFactory, VectorQuantityRecorderFactory>();

        services.AddSingleton<INegativeMagnitudeBehaviourParser, NegativeMagnitudeBehaviourParser>();
        services.AddSingleton<IScalarAssociationParser, ScalarAssociationParser>();
        services.AddSingleton<ISpecializedVectorGroupParser, SpecializedVectorGroupParser>();
        services.AddSingleton<ISpecializedVectorQuantityParser, SpecializedVectorQuantityParser>();
        services.AddSingleton<IVectorComponentNamesParser, VectorComponentNamesParser>();
        services.AddSingleton<IVectorGroupParser, VectorGroupParser>();
        services.AddSingleton<IVectorGroupMemberParser, VectorGroupMemberParser>();
        services.AddSingleton<IVectorQuantityParser, VectorQuantityParser>();
    }
}
