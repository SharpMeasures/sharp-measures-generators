namespace SharpMeasures.Generators.Types;

using SharpMeasures.Generators.Attributes.Documentation;

/// <summary>Represents a SharpMeasures type.</summary>
public interface ISharpMeasuresType : ISemanticSharpMeasuresType
{
    /// <summary>The <see cref="DisableDocumentationAttribute"/> applied to the type.</summary>
    new public abstract IDisableDocumentationRecord? DisableDocumentation { get; }

    /// <summary>The <see cref="EnableDocumentationAttribute"/> applied to the type.</summary>
    new public abstract IEnableDocumentationRecord? EnableDocumentation { get; }
}
