namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="IDisableQuantitySumRecord"/>.</summary>
public interface IDisableQuantitySumRecordFactory
{
    /// <summary>Creates a <see cref="IDisableQuantitySumRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IDisableQuantitySumRecord"/>.</returns>
    public abstract IDisableQuantitySumRecord Create(AttributeSyntax attributeSyntax);
}
