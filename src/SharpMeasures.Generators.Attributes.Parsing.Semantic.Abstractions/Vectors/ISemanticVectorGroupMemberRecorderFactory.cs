﻿namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface ISemanticVectorGroupMemberRecorderFactory
{
    /// <summary>Creates a <see cref="ISemanticRecorder{TRecord}"/> recording the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <returns>The created <see cref="ISemanticRecorder{TRecord}"/>.</returns>
    public abstract ISemanticRecorder<ISemanticVectorGroupMemberRecord> Create();
}
