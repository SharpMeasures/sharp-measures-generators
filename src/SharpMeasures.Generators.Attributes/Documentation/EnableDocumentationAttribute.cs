namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures types, declaring that XML documentation should be generated for the marked type.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EnableDocumentationAttribute : Attribute
{
    /// <summary>Declares that documentation should be generated for the marked type.</summary>
    public EnableDocumentationAttribute() { }
}
