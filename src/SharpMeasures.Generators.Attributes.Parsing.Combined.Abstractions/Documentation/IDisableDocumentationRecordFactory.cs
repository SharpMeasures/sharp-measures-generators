namespace SharpMeasures.Generators.Attributes.Parsing.Documentation;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Documentation;

/// <summary>Handles creation of <see cref="IDisableDocumentationRecord"/>.</summary>
public interface IDisableDocumentationRecordFactory
{
    /// <summary>Creates a <see cref="IDisableDocumentationRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IDisableDocumentationRecord"/>.</returns>
    public abstract IDisableDocumentationRecord Create(AttributeSyntax attributeSyntax);
}
