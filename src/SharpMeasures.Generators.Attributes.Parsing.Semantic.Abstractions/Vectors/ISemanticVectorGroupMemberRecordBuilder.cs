namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticVectorGroupMemberRecord"/>.</summary>
public interface ISemanticVectorGroupMemberRecordBuilder : IRecordBuilder<ISemanticVectorGroupMemberRecord>
{
    /// <summary>Specifies the group that the member belongs to.</summary>
    /// <param name="group">The group that the member belongs to.</param>
    public abstract void WithGroup(ITypeSymbol group);

    /// <summary>Specifies the dimension of the vector space.</summary>
    /// <param name="dimension">The dimension of the vector space.</param>
    public abstract void WithDimension(int dimension);
}
