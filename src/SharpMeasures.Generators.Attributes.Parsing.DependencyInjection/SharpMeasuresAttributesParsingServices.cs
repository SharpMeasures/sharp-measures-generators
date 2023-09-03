namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}
