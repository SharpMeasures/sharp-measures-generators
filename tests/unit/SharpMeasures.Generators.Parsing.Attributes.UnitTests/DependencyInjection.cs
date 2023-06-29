namespace SharpMeasures.Generators.Parsing.Attributes;

using Microsoft.Extensions.DependencyInjection;

using SharpMeasures.Generators.Parsing.Attributes.Extensions;

using System;

internal static class DependencyInjection
{
    private static IServiceProvider? Provider { get; set; }

    private static IServiceProvider GetProvider()
    {
        if (Provider is not null)
        {
            return Provider;
        }

        ServiceCollection services = new();

        services.AddSharpMeasuresAttributesParsing();

        Provider = services.BuildServiceProvider();

        return Provider;
    }

    public static T GetRequiredService<T>() where T : notnull => GetProvider().GetRequiredService<T>();
}
