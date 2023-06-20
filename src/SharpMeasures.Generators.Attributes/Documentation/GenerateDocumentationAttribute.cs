namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures types, describing how documentation should be generated for the type.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class GenerateDocumentationAttribute : Attribute
{
    /// <summary>Determines whether documentation should be generated for the type. The default behaviour is <see langword="true"/>.</summary>
    public bool Generate { get; }

    /// <inheritdoc cref="GenerateDocumentationAttribute"/>
    /// <param name="generate"><inheritdoc cref="Generate" path="/summary"/></param>
    public GenerateDocumentationAttribute(bool generate)
    {
        Generate = generate;
    }

    /// <inheritdoc cref="GenerateDocumentationAttribute"/>
    public GenerateDocumentationAttribute() { }
}
