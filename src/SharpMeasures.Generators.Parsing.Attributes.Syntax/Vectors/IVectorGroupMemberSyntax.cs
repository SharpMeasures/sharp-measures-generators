namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface IVectorGroupMemberSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the group that the member belongs to.</summary>
    public abstract Location Group { get; }

    /// <summary>The <see cref="Location"/> of the argument for the dimension of the quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Dimension { get; }
}
