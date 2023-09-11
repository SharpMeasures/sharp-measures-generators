namespace SharpMeasures.Generators.Members.Parsing;

using Microsoft.Extensions.DependencyInjection;

using SharpMeasures.Generators.Members.Parsing.Quantities;
using SharpMeasures.Generators.Members.Parsing.Units;

using System;

/// <summary>Allows the services of <i>SharpMeasures.Generators.Members.Parsing.Combined</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpMeasuresCombinedMembersParsingServices
{
    /// <summary>Registers the services of <i>SharpMeasures.Generators.Members.Parsing.Combined</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpMeasuresCombinedMembersParsing(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IQuantityConstantMemberParser, QuantityConstantMemberParser>();
        services.AddSingleton<IUnitInstanceMemberParser, UnitInstanceMemberParser>();

        return services;
    }
}
