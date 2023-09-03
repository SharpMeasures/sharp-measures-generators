namespace SharpMeasures.Generators.Attributes.Parsing.Documentation;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Documentation;

using System;

/// <inheritdoc cref="IDisableDocumentationRecordFactory"/>
public sealed class DisableDocumentationRecordFactory : IDisableDocumentationRecordFactory
{
    IDisableDocumentationRecord IDisableDocumentationRecordFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticDisableDocumentationRecord syntactic = new(attributeSyntax);

        return new DisableDocumentationRecord(syntactic);
    }

    private sealed class DisableDocumentationRecord : IDisableDocumentationRecord
    {
        public SyntacticDisableDocumentationRecord Syntactic { get; }

        public DisableDocumentationRecord(SyntacticDisableDocumentationRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticDisableDocumentationRecord IDisableDocumentationRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticDisableDocumentationRecord : ASyntacticRecord, ISyntacticDisableDocumentationRecord
    {
        public SyntacticDisableDocumentationRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
