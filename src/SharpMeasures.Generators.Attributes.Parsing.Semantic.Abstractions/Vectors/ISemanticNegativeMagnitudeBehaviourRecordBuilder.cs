namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

/// <summary>Handles incremental construction of <see cref="ISemanticNegativeMagnitudeBehaviourRecord"/>.</summary>
public interface ISemanticNegativeMagnitudeBehaviourRecordBuilder : IRecordBuilder<ISemanticNegativeMagnitudeBehaviourRecord>
{
    /// <summary>Specifies the behaviour of the constructor of the quantity.</summary>
    /// <param name="behaviour">The behaviour of the constructor of the quantity.</param>
    public abstract void WithBehaviour(DisallowNegativeBehaviour behaviour);
}
