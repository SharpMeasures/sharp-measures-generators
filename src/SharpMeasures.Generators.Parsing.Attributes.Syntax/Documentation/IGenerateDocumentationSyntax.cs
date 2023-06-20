namespace SharpMeasures.Generators.Parsing.Attributes.Documentation;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="GenerateDocumentationAttribute"/>.</summary>
public interface IGenerateDocumentationSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for whether documentation should be generated. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Generate { get; }
}
