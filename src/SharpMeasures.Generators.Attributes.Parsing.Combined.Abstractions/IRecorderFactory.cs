namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;

/// <summary>Handles creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of some attribute.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface IRecorderFactory<out TRecord>
{
    /// <summary>Creates a <see cref="ICombinedRecorder{TRecord}"/>.</summary>
    /// <param name="attributeSyntax">The syntactic description of the attribute.</param>
    /// <returns>The created <see cref="ICombinedRecorder{TRecord}"/>.</returns>
    public abstract ICombinedRecorder<TRecord> Create(AttributeSyntax attributeSyntax);
}
