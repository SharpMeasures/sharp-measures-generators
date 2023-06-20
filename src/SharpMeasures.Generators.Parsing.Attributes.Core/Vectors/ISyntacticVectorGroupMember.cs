namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>, with syntactical information.</summary>
public interface ISyntacticVectorGroupMember : IVectorGroupMember
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    public abstract IVectorGroupMemberSyntax Syntax { get; }
}
