namespace SharpMeasures;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>Applied to SharpMeasures quantities, describing a custom process implemented by the quantity.</summary>
/// <typeparam name="TResult">The type that is the result of the process.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Used when interpreting the attribute.")]
public sealed class QuantityProcessAttribute<TResult> : Attribute
{
    /// <summary>The name of the process.</summary>
    public string Name { get; }

    /// <summary>The expression describing the process. Occurrences of "{k}" are replaced by the k-th parameter in the provided signature.</summary>
    public string Expression { get; }

    /// <summary>The signature of the process.</summary>
    public Type[] Signature { get; }

    /// <summary>The names of the parameters of the process, or <see langword="null"/> to derive the name of each parameter from the type of the parameter.</summary>
    /// <remarks>A <see langword="null"/> element indicates that the name of that parameter should be derived from the type of the parameter.</remarks>
    public string?[]? ParameterNames { get; }

    /// <summary>Indicates that the process should be implemented statically. The default behaviour is <see langword="false"/>.</summary>
    public bool ImplementStatically { get; init; }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression)
    {
        Name = name;
        Expression = expression;

        Signature = Array.Empty<Type>();
        ParameterNames = Array.Empty<string>();
    }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression, Type[] signature)
    {
        Name = name;
        Expression = expression;

        Signature = signature;
        ParameterNames = Array.Empty<string>();
    }

    /// <inheritdoc cref="QuantityProcessAttribute{TResult}"/>
    /// <param name="name"><inheritdoc cref="Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    /// <param name="parameterNames"><inheritdoc cref="ParameterNames" path="/summary"/></param>
    public QuantityProcessAttribute(string name, string expression, Type[] signature, string?[]? parameterNames)
    {
        Name = name;
        Expression = expression;

        Signature = signature;
        ParameterNames = parameterNames;
    }
}
