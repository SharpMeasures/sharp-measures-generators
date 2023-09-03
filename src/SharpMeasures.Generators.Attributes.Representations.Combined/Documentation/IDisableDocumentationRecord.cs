namespace SharpMeasures.Generators.Attributes.Documentation;

/// <summary>Represents a <see cref="DisableDocumentationAttribute"/>.</summary>
public interface IDisableDocumentationRecord
{
    /// <summary>Represents syntactic information about the <see cref="DisableDocumentationAttribute"/>.</summary>
    public abstract ISyntacticDisableDocumentationRecord Syntactic { get; }
}
