namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using System;

/// <summary>An abstract <see cref="IParser{TRecord}"/>, parsing the arguments of some attribute.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public abstract class AParser<TRecord> : IParser<TRecord>
{
    private ICombinedParser Parser { get; }
    private IRecorderFactory<TRecord> RecorderFactory { get; }

    /// <summary>Instantiates a <see cref="AParser{TRecord}"/>, parsing the arguments of some attribute.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of attributes.</param>
    protected AParser(ICombinedParser parser, IRecorderFactory<TRecord> recorderFactory)
    {
        Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        RecorderFactory = recorderFactory ?? throw new ArgumentNullException(nameof(recorderFactory));
    }

    TRecord? IParser<TRecord>.TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recorder = RecorderFactory.Create(attributeSyntax);

        if (Parser.TryParse(recorder, attributeData, attributeSyntax) is false)
        {
            return default;
        }

        return recorder.GetRecord();
    }
}
