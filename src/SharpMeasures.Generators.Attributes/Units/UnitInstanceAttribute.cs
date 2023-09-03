namespace SharpMeasures;

using System;

/// <summary>Declares the marked property as an instance of the containing unit.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class UnitInstanceAttribute : Attribute
{
    /// <summary>The name of the unit. If ignored, the name of the property is used.</summary>
    public string? Name { get; init; }

    /// <summary>The plural form of the name of the unit. If ignored, the unaltered singular form is used.</summary>
    public string? PluralForm { get; init; }

    /// <summary>Declares the marked property as an instance of the containing unit.</summary>
    public UnitInstanceAttribute() { }
}
