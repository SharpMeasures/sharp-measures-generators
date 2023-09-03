namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IDisableQuantitySumRecordFactory"/>
public sealed class DisableQuantitySumRecordFactory : IDisableQuantitySumRecordFactory
{
    IDisableQuantitySumRecord IDisableQuantitySumRecordFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticDisableQuantitySumRecord syntactic = new(attributeSyntax);

        return new DisableQuantitySumRecord(syntactic);
    }

    private sealed class DisableQuantitySumRecord : IDisableQuantitySumRecord
    {
        public SyntacticDisableQuantitySumRecord Syntactic { get; }

        public DisableQuantitySumRecord(SyntacticDisableQuantitySumRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticDisableQuantitySumRecord IDisableQuantitySumRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticDisableQuantitySumRecord : ASyntacticRecord, ISyntacticDisableQuantitySumRecord
    {
        public SyntacticDisableQuantitySumRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
