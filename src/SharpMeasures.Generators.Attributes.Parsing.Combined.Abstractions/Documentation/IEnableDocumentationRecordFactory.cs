namespace SharpMeasures.Generators.Attributes.Parsing.Documentation;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Documentation;

/// <summary>Handles creation of <see cref="IEnableDocumentationRecord"/>.</summary>
public interface IEnableDocumentationRecordFactory
{
    /// <summary>Creates a <see cref="IEnableDocumentationRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IEnableDocumentationRecord"/>.</returns>
    public abstract IEnableDocumentationRecord Create(AttributeSyntax attributeSyntax);
}
