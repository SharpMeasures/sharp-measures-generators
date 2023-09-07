namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

/// <summary>Parses the arguments of some attribute.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface ISemanticParser<out TRecord>
{
    /// <summary>Attempts to parse the arguments of an attribute.</summary>
    /// <param name="attributeData">The semantic description of the attribute.</param>
    /// <returns>The data record representing the recorded arguments, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract TRecord? TryParse(AttributeData attributeData);
}
