namespace SharpMeasures.Generators.Attributes.Identification;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services of <i>SharpMeasures.Generators.Attributes.Identification</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpMeasuresAttributesIdentificationServices
{
    /// <summary>Registers the services of <i>SharpMeasures.Generators.Attributes.Identification</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresAttributesIdentification(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IAttributeIdentifier, AttributeIdentifier>();
        services.AddSingleton<IAttributeFilter, AttributeFilter>();

        return services;
    }
}
