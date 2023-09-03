namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
public interface ISemanticSpecializedUnitlessQuantityRecorderFactory : ISemanticRecorderFactory<ISemanticSpecializedUnitlessQuantityRecord> { }
