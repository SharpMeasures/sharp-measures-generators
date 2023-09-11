namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpAttributeParser;

/// <inheritdoc cref="ITypeConversionParser"/>
public sealed class TypeConversionParser : AParser<ITypeConversionRecord>, ITypeConversionParser
{
    /// <summary>Instantiates a <see cref="TypeConversionParser"/>, parsing the arguments of <see cref="TypeConversionAttribute"/>.</summary>
    /// <param name="parser">The parser used to parse attributes.</param>
    /// <param name="recorderFactory">Creates the recorders that record the arguments of attributes.</param>
    public TypeConversionParser(ICombinedParser parser, ITypeConversionRecorderFactory recorderFactory) : base(parser, recorderFactory) { }
}
