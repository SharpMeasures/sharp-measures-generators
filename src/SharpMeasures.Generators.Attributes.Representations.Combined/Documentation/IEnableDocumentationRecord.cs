namespace SharpMeasures.Generators.Attributes.Documentation;

/// <summary>Represents a <see cref="EnableDocumentationAttribute"/>.</summary>
public interface IEnableDocumentationRecord
{
    /// <summary>Represents syntactic information about the <see cref="EnableDocumentationAttribute"/>.</summary>
    public abstract ISyntacticEnableDocumentationRecord Syntactic { get; }
}
