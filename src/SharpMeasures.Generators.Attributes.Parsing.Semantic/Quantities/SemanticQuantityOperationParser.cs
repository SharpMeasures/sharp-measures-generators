namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="ISemanticQuantityOperationParser"/>
public sealed class SemanticQuantityOperationParser : ASemanticParser<ISemanticQuantityOperationRecord>, ISemanticQuantityOperationParser
{
    /// <summary>Instantiates a <see cref="SemanticQuantityOperationParser"/>, parsing the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public SemanticQuantityOperationParser(ISemanticParser parser, ISemanticQuantityOperationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
