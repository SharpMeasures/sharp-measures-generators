﻿namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.Extensions.DependencyInjection;

using SharpAttributeParser.Mappers;

internal static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSharpAttributeParserMappers();
    }
}
