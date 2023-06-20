﻿namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures units, describing how unit instances may be derived from instances of other units.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class UnitDerivationAttribute : Attribute
{
    /// <summary>The unique ID of this derivation - which may be <see langword="null"/> if this is the only derivation defined by the unit.</summary>
    public string? DerivationID { get; }

    /// <summary>The expression used to derive new instances of this unit. Occurrences of "{k}" are replaced by the k-th unit in the provided signature.</summary>
    /// <remarks>Some common expressions are defined in <see cref="CommonAlgebraicExpressions"/>.</remarks>
    public string Expression { get; }

    /// <summary>The units used to derive instances of this unit, according to the provided expression.</summary>
    public Type[] Signature { get; }

    /// <summary>The name of the method deriving the unit. By default, the name will be <i>From</i>.</summary>
    public string? MethodName { get; init; }

    /// <inheritdoc cref="UnitDerivationAttribute"/>
    /// <param name="derivationID"><inheritdoc cref="DerivationID" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public UnitDerivationAttribute(string? derivationID, string expression, params Type[] signature)
    {
        DerivationID = derivationID;
        Expression = expression;
        Signature = signature;
    }

    /// <inheritdoc cref="UnitDerivationAttribute"/>
    /// <param name="expression"><inheritdoc cref="Expression" path="/summary"/><para><inheritdoc cref="Expression" path="/remarks"/></para></param>
    /// <param name="signature"><inheritdoc cref="Signature" path="/summary"/></param>
    public UnitDerivationAttribute(string expression, params Type[] signature)
    {
        DerivationID = null;
        Expression = expression;
        Signature = signature;
    }
}
