namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="IDisableQuantityDifferenceRecord"/>.</summary>
public interface IDisableQuantityDifferenceRecordFactory
{
    /// <summary>Creates a <see cref="IDisableQuantityDifferenceRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IDisableQuantityDifferenceRecord"/>.</returns>
    public abstract IDisableQuantityDifferenceRecord Create(AttributeSyntax attributeSyntax);
}
