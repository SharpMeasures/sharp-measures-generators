namespace SharpMeasures.Generators.Attributes;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about an attribute.</summary>
public interface ISyntacticRecord
{
    /// <summary>The syntactic description of the entire attribute.</summary>
    public abstract AttributeSyntax Attribute { get; }
}
