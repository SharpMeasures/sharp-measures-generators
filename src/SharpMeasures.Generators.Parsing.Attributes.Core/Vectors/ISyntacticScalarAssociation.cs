namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

/// <summary>Represents a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>, with syntactical information.</summary>
public interface ISyntacticScalarAssociation : IScalarAssociation
{
    /// <summary>Provides syntactical information about the parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    public abstract IScalarAssociationSyntax Syntax { get; }
}
