namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="IUnitlessQuantityRecordFactory"/>
public sealed class UnitlessQuantityRecordFactory : IUnitlessQuantityRecordFactory
{
    IUnitlessQuantityRecord IUnitlessQuantityRecordFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticUnitlessQuantityRecord syntactic = new(attributeSyntax);

        return new UnitlessQuantityRecord(syntactic);
    }

    private sealed class UnitlessQuantityRecord : IUnitlessQuantityRecord
    {
        public SyntacticUnitlessQuantityRecord Syntactic { get; }

        public UnitlessQuantityRecord(SyntacticUnitlessQuantityRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticUnitlessQuantityRecord IUnitlessQuantityRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticUnitlessQuantityRecord : ASyntacticRecord, ISyntacticUnitlessQuantityRecord
    {
        public SyntacticUnitlessQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
