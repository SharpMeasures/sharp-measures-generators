namespace SharpMeasures;

using System;

/// <summary>Describes how a type implements a conversion.</summary>
[Flags]
public enum ConversionImplementation
{
    /// <summary>Indicates that the conversion is not implemented.</summary>
    None = 0,
    /// <summary>The conversion is implemented as a property.</summary>
    Property = 1,
    /// <summary>The conversion is implemented as an instance method.</summary>
    InstanceMethod = 2,
    /// <summary>The conversion is implemented as a static method.</summary>
    StaticMethod = 4,
    /// <summary>The conversion is implemented as an operator.</summary>
    Operator = 8,
    /// <summary>The conversion is implemented as a property, an instance method, a static  method, and an operator.</summary>
    All = Property | InstanceMethod | StaticMethod | Operator
}
