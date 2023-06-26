namespace SharpMeasures.Generators.Parsing.Attributes.Extensions;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Extensions;

using SharpMeasures.Generators.Parsing.Attributes.Documentation;
using SharpMeasures.Generators.Parsing.Attributes.Quantities;
using SharpMeasures.Generators.Parsing.Attributes.Scalars;
using SharpMeasures.Generators.Parsing.Attributes.Units;
using SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Allows the services offered by <i>SharpMeasures.Generators.Parsing.Attributes</i> to be registered with <see cref="IServiceCollection"/>.</summary>
public static class AttributesParsingServicesRegistration
{
    /// <summary>Registers services offered by <i>SharpMeasures.Generators.Parsing.Attributes</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> that the services are registered with.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresAttributesParsing(this IServiceCollection services)
    {
        services.AddSharpAttributeParser();

        AddParser<ISyntacticGenerateDocumentationParser, ISemanticGenerateDocumentationParser, GenerateDocumentationParser>(services);

        AddSharpMeasuresUnitAttributesParsing(services);
        AddSharpMeasuresQuantityAttributesParsing(services);
        AddSharpMeasuresScalarAttributesParsing(services);
        AddSharpMeasuresVectorAttributesParsing(services);

        return services;
    }

    private static void AddSharpMeasuresUnitAttributesParsing(IServiceCollection services)
    {
        AddParser<ISyntacticUnitParser, ISemanticUnitParser, UnitParser>(services);
        AddParser<ISyntacticUnitDerivationParser, ISemanticUnitDerivationParser, UnitDerivationParser>(services);
        AddParser<ISyntacticFixedUnitInstanceParser, ISemanticFixedUnitInstanceParser, FixedUnitInstanceParser>(services);
        AddParser<ISyntacticPrefixedUnitInstanceParser, ISemanticPrefixedUnitInstanceParser, PrefixedUnitInstanceParser>(services);
        AddParser<ISyntacticScaledUnitInstanceParser, ISemanticScaledUnitInstanceParser, ScaledUnitInstanceParser>(services);
        AddParser<ISyntacticBiasedUnitInstanceParser, ISemanticBiasedUnitInstanceParser, BiasedUnitInstanceParser>(services);
        AddParser<ISyntacticAliasedUnitInstanceParser, ISemanticAliasedUnitInstanceParser, AliasedUnitInstanceParser>(services);
        AddParser<ISyntacticDerivedUnitInstanceParser, ISemanticDerivedUnitInstanceParser, DerivedUnitInstanceParser>(services);
    }

    private static void AddSharpMeasuresQuantityAttributesParsing(IServiceCollection services)
    {
        AddParser<ISyntacticDefaultUnitInstanceParser, ISemanticDefaultUnitInstanceParser, DefaultUnitInstanceParser>(services);
        AddParser<ISyntacticDisableQuantityDifferenceParser, ISemanticDisableQuantityDifferenceParser, DisableQuantityDifferenceParser>(services);
        AddParser<ISyntacticDisableQuantitySumParser, ISemanticDisableQuantitySumParser, DisableQuantitySumParser>(services);
        AddParser<ISyntacticQuantityDifferenceParser, ISemanticQuantityDifferenceParser, QuantityDifferenceParser>(services);
        AddParser<ISyntacticQuantityConversionParser, ISemanticQuantityConversionParser, QuantityConversionParser>(services);
        AddParser<ISyntacticQuantityOperationParser, ISemanticQuantityOperationParser, QuantityOperationParser>(services);
        AddParser<ISyntacticQuantityProcessParser, ISemanticQuantityProcessParser, QuantityProcessParser>(services);
        AddParser<ISyntacticQuantityPropertyParser, ISemanticQuantityPropertyParser, QuantityPropertyParser>(services);
        AddParser<ISyntacticQuantitySumParser, ISemanticQuantitySumParser, QuantitySumParser>(services);
    }

    private static void AddSharpMeasuresScalarAttributesParsing(IServiceCollection services)
    {
        AddParser<ISyntacticDisallowNegativeParser, ISemanticDisallowNegativeParser, DisallowNegativeParser>(services);
        AddParser<ISyntacticScalarConstantParser, ISemanticScalarConstantParser, ScalarConstantParser>(services);
        AddParser<ISyntacticScalarQuantityParser, ISemanticScalarQuantityParser, ScalarQuantityParser>(services);
        AddParser<ISyntacticSpecializedScalarQuantityParser, ISemanticSpecializedScalarQuantityParser, SpecializedScalarQuantityParser>(services);
        AddParser<ISyntacticSpecializedUnitlessQuantityParser, ISemanticSpecializedUnitlessQuantityParser, SpecializedUnitlessQuantityParser>(services);
        AddParser<ISyntacticUnitlessQuantityParser, ISemanticUnitlessQuantityParser, UnitlessQuantityParser>(services);
        AddParser<ISyntacticVectorAssociationParser, ISemanticVectorAssociationParser, VectorAssociationParser>(services);
    }

    private static void AddSharpMeasuresVectorAttributesParsing(IServiceCollection services)
    {
        AddParser<ISyntacticScalarAssociationParser, ISemanticScalarAssociationParser, ScalarAssociationParser>(services);
        AddParser<ISyntacticSpecializedVectorGroupParser, ISemanticSpecializedVectorGroupParser, SpecializedVectorGroupParser>(services);
        AddParser<ISyntacticSpecializedVectorQuantityParser, ISemanticSpecializedVectorQuantityParser, SpecializedVectorQuantityParser>(services);
        AddParser<ISyntacticVectorComponentNamesParser, ISemanticVectorComponentNamesParser, VectorComponentNamesParser>(services);
        AddParser<ISyntacticVectorConstantParser, ISemanticVectorConstantParser, VectorConstantParser>(services);
        AddParser<ISyntacticVectorGroupParser, ISemanticVectorGroupParser, VectorGroupParser>(services);
        AddParser<ISyntacticVectorGroupMemberParser, ISemanticVectorGroupMemberParser, VectorGroupMemberParser>(services);
        AddParser<ISyntacticVectorQuantityParser, ISemanticVectorQuantityParser, VectorQuantityParser>(services);
    }

    private static void AddParser<TSyntactic, TSemantic, TImplementation>(IServiceCollection services) where TSyntactic : class where TSemantic : class where TImplementation : class, TSyntactic, TSemantic
    {
        services.AddSingleton<TSyntactic, TImplementation>();
        services.AddSingleton<TSemantic, TImplementation>();
    }
}
