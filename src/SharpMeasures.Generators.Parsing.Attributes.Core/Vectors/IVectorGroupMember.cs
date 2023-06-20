namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface IVectorGroupMember
{
    /// <summary>The group that the member belongs to.</summary>
    public abstract ITypeSymbol Group { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract int? Dimension { get; }
}
