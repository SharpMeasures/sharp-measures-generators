namespace SharpMeasures.Generators.Members.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Represents syntactic information about an instance of some unit.</summary>
public interface ISyntacticUnitInstanceMember
{
    /// <summary>The syntactic description of the property that defines the unit instance.</summary>
    public abstract PropertyDeclarationSyntax Property { get; }

    /// <summary>The syntactic description of the attribute that marks the member as a unit instance.</summary>
    public abstract ISyntacticUnitInstanceRecord Attribute { get; }
}
