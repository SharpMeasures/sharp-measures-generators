namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="DisallowNegativeAttribute"/>.</summary>
public interface IDisallowNegativeRecorderFactory : IRecorderFactory<IDisallowNegativeRecord> { }
