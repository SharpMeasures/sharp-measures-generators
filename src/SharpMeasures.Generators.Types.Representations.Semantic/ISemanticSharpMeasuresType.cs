namespace SharpMeasures.Generators.Types;

using Microsoft.CodeAnalysis;

/// <summary>Represents a SharpMeasures type.</summary>
public interface ISemanticSharpMeasuresType
{
    /// <summary>The symbol associated with the type.</summary>
    public abstract ITypeSymbol Type { get; }

    /// <summary>Indicates whether the type was marked with a <see cref="DisableDocumentationAttribute"/>.</summary>
    public abstract bool DisableDocumentation { get; }

    /// <summary>Indicates whether the type was marked with a <see cref="EnableDocumentationAttribute"/>.</summary>
    public abstract bool EnableDocumentation { get; }
}
