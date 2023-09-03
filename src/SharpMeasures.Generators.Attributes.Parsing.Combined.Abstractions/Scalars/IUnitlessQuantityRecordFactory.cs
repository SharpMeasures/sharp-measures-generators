namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles creation of <see cref="IUnitlessQuantityRecord"/>.</summary>
public interface IUnitlessQuantityRecordFactory
{
    /// <summary>Creates a <see cref="IUnitlessQuantityRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IUnitlessQuantityRecord"/>.</returns>
    public abstract IUnitlessQuantityRecord Create(AttributeSyntax attributeSyntax);
}
