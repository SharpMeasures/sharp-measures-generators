namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <inheritdoc cref="IQuantityOperationParser"/>
public sealed class QuantityOperationParser : AParser<IQuantityOperationRecord>, IQuantityOperationParser
{
    /// <summary>Instantiates a <see cref="QuantityOperationParser"/>, parsing the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public QuantityOperationParser(ICombinedParser parser, IQuantityOperationRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
