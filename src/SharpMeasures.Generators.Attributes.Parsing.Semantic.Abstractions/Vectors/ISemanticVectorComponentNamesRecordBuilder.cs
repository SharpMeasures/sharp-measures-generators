namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Vectors;

using System.Collections.Generic;

/// <summary>Handles incremental construction of <see cref="ISemanticVectorComponentNamesRecord"/>.</summary>
public interface ISemanticVectorComponentNamesRecordBuilder : IRecordBuilder<ISemanticVectorComponentNamesRecord>
{
    /// <summary>Specifies the names of the Cartesian components.</summary>
    /// <param name="names">The names of the Cartesian components.</param>
    public abstract void WithNames(IReadOnlyList<string?>? names);

    /// <summary>Specifies the expression used to derive the name of each Cartesian component.</summary>
    /// <param name="expression">The expression used to derive the name of each Cartesian component.</param>
    public abstract void WithExpression(string? expression);
}
