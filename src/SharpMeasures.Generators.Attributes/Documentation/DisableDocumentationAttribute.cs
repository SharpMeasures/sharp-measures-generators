namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures types, declaring that XML documentation should not be generated for the marked type.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class DisableDocumentationAttribute : Attribute
{
    /// <summary>Declares that documentation should not be generated for the marked type.</summary>
    public DisableDocumentationAttribute() { }
}
