namespace SharpMeasures.Generators.Parsing.Attributes;

using System;
using System.Collections.Generic;
using System.Globalization;

internal static class StringRepresentationFactory
{
    public static string Create(bool? value) => value switch
    {
        null => "(bool?)null",
        true => "true",
        false => "false"
    };

    public static string Create(int? value) => value switch
    {
        null => "(int?)null",
        not null => value.Value.ToString(CultureInfo.InvariantCulture)
    };

    public static string Create(string? value) => value switch
    {
        null => "(string)null",
        not null => $"\"{value}\""
    };

    public static string Create(string type, IEnumerable<string?>? values)
    {
        if (values is null)
        {
            return $"({type}[])null";
        }

        return $$"""new {{type}}[] { {{string.Join(", ", values)}} }""";
    }

    public static string CreateEnum<TEnum>(TEnum? value)
    {
        if (value is null)
        {
            return $"({typeof(TEnum).FullName}?)null";
        }

        if (Enum.IsDefined(typeof(TEnum), value))
        {
            return $"{typeof(TEnum).FullName}.{value}";
        }

        return $"({typeof(TEnum).FullName})({value})";
    }
}
