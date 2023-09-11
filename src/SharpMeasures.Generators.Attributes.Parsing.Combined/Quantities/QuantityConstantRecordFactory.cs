namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IQuantityConstantRecordFactory"/>
public sealed class QuantityConstantRecordFactory : IQuantityConstantRecordFactory
{
    IQuantityConstantRecord IRecordFactory<IQuantityConstantRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticQuantityConstantRecord syntactic = new(attributeSyntax);

        return new QuantityConstantRecord(syntactic);
    }

    private sealed class QuantityConstantRecord : IQuantityConstantRecord
    {
        public SyntacticQuantityConstantRecord Syntactic { get; }

        public QuantityConstantRecord(SyntacticQuantityConstantRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticQuantityConstantRecord IQuantityConstantRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticQuantityConstantRecord : ASyntacticRecord, ISyntacticQuantityConstantRecord
    {
        public SyntacticQuantityConstantRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
