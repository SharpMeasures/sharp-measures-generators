namespace SharpMeasures.Generators.Members.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Represents syntactic information about a constant of some quantity.</summary>
public interface ISyntacticQuantityConstantMember
{
    /// <summary>The syntactic description of the property that defines the constant.</summary>
    public abstract PropertyDeclarationSyntax Property { get; }

    /// <summary>The syntactic description of the attribute that marks the member as a constant.</summary>
    public abstract ISyntacticQuantityConstantRecord Attribute { get; }
}
