namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="IAllowNegativeRecordFactory"/>
public sealed class AllowNegativeRecordFactory : IAllowNegativeRecordFactory
{
    IAllowNegativeRecord IRecordFactory<IAllowNegativeRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticAllowNegativeRecord syntactic = new(attributeSyntax);

        return new AllowNegativeRecord(syntactic);
    }

    private sealed class AllowNegativeRecord : IAllowNegativeRecord
    {
        public SyntacticAllowNegativeRecord Syntactic { get; }

        public AllowNegativeRecord(SyntacticAllowNegativeRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticAllowNegativeRecord IAllowNegativeRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticAllowNegativeRecord : ASyntacticRecord, ISyntacticAllowNegativeRecord
    {
        public SyntacticAllowNegativeRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
