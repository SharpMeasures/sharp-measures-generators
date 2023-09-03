namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Handles creation of data records representing some attribute.</summary>
/// <typeparam name="TRecord">The type of the created data record.</typeparam>
public interface IRecordFactory<out TRecord>
{
    /// <summary>Creates a data record, representing an attribute.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created data record.</returns>
    public abstract TRecord Create(AttributeSyntax attributeSyntax);
}
