﻿namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface IScalarAssociationSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the scalar quantity associated with the vector quantity.</summary>
    public abstract Location ScalarQuantity { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the scalar quantity should be used to describe each Cartesian component of the vector quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location AsComponents { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the scalar quantity should be used to describe the magnitude of the vector quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location AsMagnitude { get; }
}
