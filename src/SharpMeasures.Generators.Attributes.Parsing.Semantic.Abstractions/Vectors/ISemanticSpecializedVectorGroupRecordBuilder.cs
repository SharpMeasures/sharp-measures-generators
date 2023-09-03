namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticSpecializedVectorGroupRecord"/>.</summary>
public interface ISemanticSpecializedVectorGroupRecordBuilder : IRecordBuilder<ISemanticSpecializedVectorGroupRecord>
{
    /// <summary>Specifies the original group.</summary>
    /// <param name="original">The original group, of which the group is a specialized form.</param>
    public abstract void WithOriginal(ITypeSymbol original);
}
