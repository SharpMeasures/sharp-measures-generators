namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IDisableQuantityDifferenceRecordFactory"/>
public sealed class DisableQuantityDifferenceRecordFactory : IDisableQuantityDifferenceRecordFactory
{
    IDisableQuantityDifferenceRecord IRecordFactory<IDisableQuantityDifferenceRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticDisableQuantityDifferenceRecord syntactic = new(attributeSyntax);

        return new DisableQuantityDifferenceRecord(syntactic);
    }

    private sealed class DisableQuantityDifferenceRecord : IDisableQuantityDifferenceRecord
    {
        public SyntacticDisableQuantityDifferenceRecord Syntactic { get; }

        public DisableQuantityDifferenceRecord(SyntacticDisableQuantityDifferenceRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticDisableQuantityDifferenceRecord IDisableQuantityDifferenceRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticDisableQuantityDifferenceRecord : ASyntacticRecord, ISyntacticDisableQuantityDifferenceRecord
    {
        public SyntacticDisableQuantityDifferenceRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
