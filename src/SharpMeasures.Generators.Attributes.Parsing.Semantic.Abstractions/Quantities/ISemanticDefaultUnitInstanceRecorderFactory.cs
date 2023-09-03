﻿namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
public interface ISemanticDefaultUnitInstanceRecorderFactory
{
    /// <summary>Creates a <see cref="ISemanticRecorder{TRecord}"/> recording the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <returns>The created <see cref="ISemanticRecorder{TRecord}"/>.</returns>
    public abstract ISemanticRecorder<ISemanticDefaultUnitInstanceRecord> Create();
}
