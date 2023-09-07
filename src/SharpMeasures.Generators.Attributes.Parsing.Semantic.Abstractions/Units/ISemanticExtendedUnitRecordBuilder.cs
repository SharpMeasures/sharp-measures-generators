namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Units;

/// <summary>Handles incremental construction of <see cref="ISemanticExtendedUnitRecord"/>.</summary>
public interface ISemanticExtendedUnitRecordBuilder : IRecordBuilder<ISemanticExtendedUnitRecord>
{
    /// <summary>Specifies the original unit.</summary>
    /// <param name="original">The original unit, of which the unit is an extensions.</param>
    public abstract void WithOriginal(ITypeSymbol original);
}
