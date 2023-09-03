namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISemanticDisallowNegativeRecord"/>.</summary>
public interface ISemanticDisallowNegativeRecordBuilder : IRecordBuilder<ISemanticDisallowNegativeRecord>
{
    /// <summary>Specifies the behaviour of the constructor of the quantity.</summary>
    /// <param name="behaviour">The behaviour of the constructor of the quantity.</param>
    public abstract void WithBehaviour(DisallowNegativeBehaviour behaviour);
}
