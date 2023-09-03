namespace SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Represents a <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
public interface IVectorGroupMemberRecord : ISemanticVectorGroupMemberRecord
{
    /// <summary>Represents syntactic information about the <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    public abstract ISyntacticVectorGroupMemberRecord Syntactic { get; }
}
