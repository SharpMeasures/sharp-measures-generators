﻿namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
public interface IScalarQuantityRecorderFactory
{
    /// <summary>Creates a <see cref="ICombinedRecorder{TRecord}"/> recording the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="ICombinedRecorder{TRecord}"/>.</returns>
    public abstract ICombinedRecorder<IScalarQuantityRecord> Create(AttributeSyntax attributeSyntax);
}