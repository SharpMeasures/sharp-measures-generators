namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
public interface ISemanticQuantityConversionRecorderFactory : ISemanticRecorderFactory<ISemanticQuantityConversionRecord> { }
