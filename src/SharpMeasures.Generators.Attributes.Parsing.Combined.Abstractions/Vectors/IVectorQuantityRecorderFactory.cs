﻿namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface IVectorQuantityRecorderFactory : IRecorderFactory<IVectorQuantityRecord> { }
