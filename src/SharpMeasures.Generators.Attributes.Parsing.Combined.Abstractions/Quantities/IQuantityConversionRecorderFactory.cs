namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
public interface IQuantityConversionRecorderFactory
{
    /// <summary>Creates a <see cref="ICombinedRecorder{TRecord}"/> recording the arguments of <see cref="QuantityConversionAttribute"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="ICombinedRecorder{TRecord}"/>.</returns>
    public abstract ICombinedRecorder<IQuantityConversionRecord> Create(AttributeSyntax attributeSyntax);
}
