namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Parses the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface ISemanticQuantityOperationParser : ISemanticParser<ISemanticQuantityOperationRecord> { }
