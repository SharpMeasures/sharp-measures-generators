namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface IQuantityDifferenceRecorderFactory : IRecorderFactory<IQuantityDifferenceRecord> { }
