namespace SharpMeasures.Generators.Parsing.Attributes.Documentation;

/// <summary>Represents a parsed <see cref="GenerateDocumentationAttribute"/>, with syntactical information.</summary>
public interface ISyntacticGenerateDocumentation : IGenerateDocumentation
{
    /// <summary>Provides syntactical information about the parsed <see cref="GenerateDocumentationAttribute"/>.</summary>
    public abstract IGenerateDocumentationSyntax Syntax { get; }
}
