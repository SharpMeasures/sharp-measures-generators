namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures vector quantities, allowing the names of the Cartesian components to be customized.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class VectorComponentNamesAttribute : Attribute
{
    /// <summary>The names of the Cartesian components.</summary>
    public string[]? Names { get; }

    /// <summary>The expression used to derive the name of each Cartesian component. Occurrences of "{i}" are replaced by the zero-based index of the Cartesian component, and "{i + 1}" by the one-based index.</summary>
    public string? Expression { get; }

    /// <inheritdoc cref="VectorComponentNamesAttribute"/>
    /// <param name="names"><inheritdoc cref="Names" path="/summary"/></param>
    public VectorComponentNamesAttribute(string[] names)
    {
        Names = names;
    }

    /// <inheritdoc cref="VectorComponentNamesAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    public VectorComponentNamesAttribute(string expression)
    {
        Expression = expression;
    }
}
