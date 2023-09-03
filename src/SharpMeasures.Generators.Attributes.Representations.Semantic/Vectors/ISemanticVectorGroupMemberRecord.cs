namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface ISemanticVectorGroupMemberRecord
{
    /// <summary>The group that the member belongs to.</summary>
    public abstract ITypeSymbol Group { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract OneOf<None, int> Dimension { get; }
}
