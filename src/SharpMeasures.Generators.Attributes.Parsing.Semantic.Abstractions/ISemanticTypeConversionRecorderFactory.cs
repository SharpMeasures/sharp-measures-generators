namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="TypeConversionAttribute"/>.</summary>
public interface ISemanticTypeConversionRecorderFactory : ISemanticRecorderFactory<ISemanticTypeConversionRecord> { }
