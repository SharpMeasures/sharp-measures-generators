namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface IQuantityOperationRecorderFactory : IRecorderFactory<IQuantityOperationRecord> { }
