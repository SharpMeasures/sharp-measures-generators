namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

/// <summary>Represents a parsed <see cref="VectorAssociationAttribute{TVector}"/>, with syntactical information.</summary>
public interface ISyntacticVectorAssociation : IVectorAssociation
{
    /// <summary>Provides syntactical information about the parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    public abstract IVectorAssociationSyntax Syntax { get; }
}
