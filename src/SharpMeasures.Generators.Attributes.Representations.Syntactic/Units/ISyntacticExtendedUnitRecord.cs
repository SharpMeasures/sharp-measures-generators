namespace SharpMeasures.Generators.Attributes.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Represents syntactic information about a <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
public interface ISyntacticExtendedUnitRecord : ISyntacticRecord
{
    /// <summary>The syntactic description of the argument for the original unit, of which this unit is an extension.</summary>
    public abstract ExpressionSyntax Original { get; }
}
