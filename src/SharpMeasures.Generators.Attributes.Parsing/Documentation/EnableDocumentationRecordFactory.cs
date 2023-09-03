namespace SharpMeasures.Generators.Attributes.Parsing.Documentation;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Documentation;

using System;

/// <inheritdoc cref="IEnableDocumentationRecordFactory"/>
public sealed class EnableDocumentationRecordFactory : IEnableDocumentationRecordFactory
{
    IEnableDocumentationRecord IEnableDocumentationRecordFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        SyntacticEnableDocumentationRecord syntactic = new(attributeSyntax);

        return new EnableDocumentationRecord(syntactic);
    }

    private sealed class EnableDocumentationRecord : IEnableDocumentationRecord
    {
        public SyntacticEnableDocumentationRecord Syntactic { get; }

        public EnableDocumentationRecord(SyntacticEnableDocumentationRecord syntactic)
        {
            Syntactic = syntactic;
        }

        ISyntacticEnableDocumentationRecord IEnableDocumentationRecord.Syntactic => Syntactic;
    }

    private sealed class SyntacticEnableDocumentationRecord : ASyntacticRecord, ISyntacticEnableDocumentationRecord
    {
        public SyntacticEnableDocumentationRecord(AttributeSyntax attribute) : base(attribute) { }
    }
}
