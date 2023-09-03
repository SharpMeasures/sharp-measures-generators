namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorGroupRecorderFactory
{
    /// <summary>Creates a <see cref="ICombinedRecorder{TRecord}"/> recording the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="ICombinedRecorder{TRecord}"/>.</returns>
    public abstract ICombinedRecorder<ISpecializedVectorGroupRecord> Create(AttributeSyntax attributeSyntax);
}
