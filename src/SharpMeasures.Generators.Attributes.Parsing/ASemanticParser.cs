namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using System;

/// <summary>An abstract <see cref="ISemanticParser{TRecord}"/>, parsing the arguments of some attribute.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public abstract class ASemanticParser<TRecord> : ISemanticParser<TRecord>
{
    private ISemanticParser Parser { get; }
    private ISemanticRecorderFactory<TRecord> RecorderFactory { get; }

    /// <summary>Instantiates a <see cref="ASemanticParser{TRecord}"/>, parsing the arguments of some attribute.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of attributes.</param>
    protected ASemanticParser(ISemanticParser parser, ISemanticRecorderFactory<TRecord> recorderFactory)
    {
        Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        RecorderFactory = recorderFactory ?? throw new ArgumentNullException(nameof(recorderFactory));
    }

    TRecord? ISemanticParser<TRecord>.TryParse(AttributeData attributeData)
    {
        if (attributeData is null)
        {
            throw new ArgumentNullException(nameof(attributeData));
        }

        var recorder = RecorderFactory.Create();

        if (Parser.TryParse(recorder, attributeData) is false)
        {
            return default;
        }

        return recorder.GetRecord();
    }
}
