namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>An abstract <see cref="ISyntacticRecord"/>, representing syntactic information about an argument.</summary>
internal abstract class ASyntacticRecord : ISyntacticRecord
{
    private AttributeSyntax Attribute { get; }

    /// <summary>Instantiates a <see cref="ASyntacticRecord"/>, representing syntactic information about an argument.</summary>
    /// <param name="attribute">The syntactic description of the entire attribute.</param>
    protected ASyntacticRecord(AttributeSyntax attribute)
    {
        Attribute = attribute;
    }

    AttributeSyntax ISyntacticRecord.Attribute => Attribute;
}
