namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles creation of <see cref="IAllowNegativeRecord"/>.</summary>
public interface IAllowNegativeRecordFactory
{
    /// <summary>Creates a <see cref="IAllowNegativeRecord"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="IAllowNegativeRecord"/>.</returns>
    public abstract IAllowNegativeRecord Create(AttributeSyntax attributeSyntax);
}
