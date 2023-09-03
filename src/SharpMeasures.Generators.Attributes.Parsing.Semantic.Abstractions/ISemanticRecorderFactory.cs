namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpAttributeParser;

/// <summary>Handles creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of some attribute.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface ISemanticRecorderFactory<out TRecord>
{
    /// <summary>Creates a <see cref="ISemanticRecorder{TRecord}"/>.</summary>
    /// <returns>The created <see cref="ISemanticRecorder{TRecord}"/>.</returns>
    public abstract ISemanticRecorder<TRecord> Create();
}
