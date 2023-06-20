namespace SharpMeasures.Generators.Parsing.Attributes.Documentation;

/// <summary>Represents a parsed <see cref="GenerateDocumentationAttribute"/>.</summary>
public interface IGenerateDocumentation
{
    /// <summary>Determines whether documentation should be generated for the type.</summary>
    public abstract bool? Generate { get; }
}
