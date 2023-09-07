namespace SharpMeasures;

using System;

/// <summary>Determines how a quantity that may not be negative behaves when constructed with a negative value.</summary>
public enum DisallowNegativeBehaviour
{
    /// <summary>The <see cref="DisallowNegativeBehaviour"/> is unknown.</summary>
    Unknown,
    /// <summary>The absolute of the provided value is used.</summary>
    Absolute,
    /// <summary>An <see cref="ArgumentException"/> is thrown.</summary>
    Exception
}
