namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface ISemanticUnitRecorderFactory : ISemanticRecorderFactory<ISemanticUnitRecord> { }
