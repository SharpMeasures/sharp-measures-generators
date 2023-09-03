namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IUnitRecorderFactory : IRecorderFactory<IUnitRecord> { }
